using System.Collections.Generic;

namespace IIMes.Services.Runtime.Interface.DTO
{
    public class ExecutionSpecDTO
    {
        public int TerminalId { get; set; }
        public string Wo { get; set; }
        public string Container { get; set; }
        public IDictionary<string, object> Values { get; set; }
        public string Editor { get; set; }
    }
}

