using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace Consumer.Interface
{
    public class ConsumerOptions : IOptions<ConsumerOptions>
    {
        public ConsumerOptions Value => this;

        public IDictionary<string, ConsumerOption> Consumers { get; set; }
    }
}