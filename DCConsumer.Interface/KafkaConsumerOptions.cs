using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace Consumer.Interface
{
    public class KafkaConsumerOptions : IOptions<KafkaConsumerOptions>
    {
        public KafkaConsumerOptions Value => this;
        public string BootstrapServers { get; set; }
        public IDictionary<string, ConsumerOption> Consumers { get; set; }
    }

    public class ConsumerOption
    {
        public string GroupId { get; set; }
        public string Topic { get; set; }
        public string FileEncoding { get; set; }
    }
}
