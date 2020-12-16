using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Consumer.Interface;

namespace DCConsumer.Service
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IEnumerable<IConsumer> _consumers;
        private readonly IOptions<KafkaConsumerOptions> _options;

        public Worker(
            ILogger<Worker> logger,
            IOptions<KafkaConsumerOptions> options,
            IEnumerable<IConsumer> consumers
            )
        {
            _logger = logger;
            _consumers = consumers;
            _options = options;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    StartConsumer(stoppingToken);
                    System.Console.WriteLine("Data collection service started");
                    while (!stoppingToken.IsCancellationRequested)
                    {
                        _logger.LogInformation("Working  {time}", DateTimeOffset.Now);
                        await Task.Delay(60000, stoppingToken);
                    }

                    // var result = _consumer.Consume(stoppingToken);


                    // if (!result.IsPartitionEOF)
                    // {
                    //     _logger.LogDebug("Consuming {0}", result.Message.Value);
                    //     DataMessage message = JsonSerializer.Deserialize<DataMessage>(result.Message.Value, new JsonSerializerOptions
                    //     {
                    //         PropertyNamingPolicy = new CustomJsonNamingPolicy()
                    //     });

                    //     var payload = message.Payload;
                    //     payload.Content = ConvertContent(payload.Content);
                    // }
                }
                catch (Exception e)
                {
                    _logger.LogError(e, string.Empty);
                }
            }
        }

        private void StartConsumer(CancellationToken stoppingToken)
        {
            foreach (var consumer in _consumers)
            {
                try
                {
                    consumer.StartConsume(stoppingToken);
                    _logger.LogInformation("{0} started", consumer.GetType().Name);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Fail to start {0}", consumer.GetType().Name);
                }
            }
        }

        public override void Dispose()
        {
            foreach (var consumer in _consumers)
            {
                consumer.Dispose();
            }
        }

    }
}
