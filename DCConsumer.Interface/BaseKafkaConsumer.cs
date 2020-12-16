using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Compression;
using System.IO;

namespace Consumer.Interface
{
    public abstract class BaseKafkaConsumer : IConsumer
    {
        protected const string LogFormat = "message:{0}, duration:{1}";
        protected IConsumer<Ignore, string> _consumer;
        protected readonly IOptions<KafkaConsumerOptions> _options;
        protected readonly ILogger<BaseKafkaConsumer> _logger;
        protected IServiceProvider _sp;

        public BaseKafkaConsumer(
                              ILogger<BaseKafkaConsumer> logger,
                              IOptions<KafkaConsumerOptions> options,
                              IServiceProvider sp
                              )
        {
            _logger = logger;
            _options = options;
            _sp = sp;
        }
        public Task Consume(string message)
        {
            var watch = Stopwatch.StartNew();
            try
            {
                // using var ctx = _contextManager.OpenDBContext();
                // _contextManager.BindDBContext(ctx);
                DoConsume(message);
            }
            catch (System.Exception e)
            {
                _logger.LogError(e, string.Empty);
            }
            finally
            {
                watch.Stop();
                _logger.LogInformation(LogFormat, message, watch.ElapsedMilliseconds);
            }
            return Task.CompletedTask;
        }

        public abstract void DoConsume(string message);
        public virtual void Dispose()
        {
            _consumer.Unsubscribe();
            _consumer.Dispose();
        }

        public void StartConsume(CancellationToken stoppingToken)
        {
            var typeName = this.GetType().Name;
            if (!_options.Value.Consumers.ContainsKey(typeName))
            {
                throw new Exception(string.Format("No consumer config for {0}", typeName));
            }
            var option = _options.Value.Consumers[typeName];
            var bootStrap = _options.Value.BootstrapServers;

            var config = new ConsumerConfig
            {
                GroupId = option.GroupId ?? Guid.NewGuid().ToString(),
                BootstrapServers = bootStrap,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = true
            };
            var builder = new ConsumerBuilder<Ignore, string>(config);
            _consumer = builder.Build();
            _consumer.Subscribe(option.Topic);
            Task.Factory.StartNew(() =>
           {
               while (!stoppingToken.IsCancellationRequested)
               {
                   var result = _consumer.Consume(stoppingToken);
                   if (!result.IsPartitionEOF)
                   {
                       _logger.LogDebug("Consuming {0}", result.Message.Value);
                       DataMessage message = JsonSerializer.Deserialize<DataMessage>(result.Message.Value, new JsonSerializerOptions
                       {
                           PropertyNamingPolicy = new CustomJsonNamingPolicy()
                       });

                       var payload = message.Payload;
                       payload.Content = ConvertContent(payload.Content, payload.Compress, option.FileEncoding);
                       DoConsume(payload.Content);
                   }
               }
           },
            stoppingToken,
            TaskCreationOptions.LongRunning,
            TaskScheduler.Default);
        }

        private static string ConvertContent(string content, bool decompress, string fileEncoding)
        {
            var byteContent = StringToByteArray(content.Substring(2));
            if (decompress)
            {
                byteContent = Decompress(byteContent);
            }
            var encoding = System.Text.Encoding.GetEncoding(fileEncoding);
            return encoding.GetString(byteContent);
        }

        private static byte[] Decompress(byte[] content)
        {
            using (GZipStream stream = new GZipStream(new MemoryStream(content),
              CompressionMode.Decompress))
            {
                const int size = 4096;
                byte[] buffer = new byte[size];
                using (MemoryStream memory = new MemoryStream())
                {
                    int count = 0;
                    do
                    {
                        count = stream.Read(buffer, 0, size);
                        if (count > 0)
                        {
                            memory.Write(buffer, 0, count);
                        }
                    }
                    while (count > 0);
                    return memory.ToArray();
                }
            }
        }

        private static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
    }
}
