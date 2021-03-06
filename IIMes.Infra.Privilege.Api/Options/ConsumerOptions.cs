using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace Consumer.Interface
{
    public class MqOptions : IOptions<MqOptions>
    {
        public MqOptions Value => this;
        public string Host { get; set; }
        public string VirtualHost { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int PrefetchCount { get; set; }
    }

    public class ConsumerOptions : IOptions<ConsumerOptions>
    {
        public ConsumerOptions Value => this;
        public IDictionary<string, ConsumerOption> Consumers { get; set; }
    }

    public class ConsumerOption
    {
        public string Exchange { get; set; }
        public string Queue { get; set; }
        public string Binding { get; set; }
    }
}
