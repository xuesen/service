using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace Consumer.Interface
{
    public class ConsumerOption
    {
        public string Exchange { get; set; }

        public string Queue { get; set; }

        public string Binding { get; set; }
    }
}
