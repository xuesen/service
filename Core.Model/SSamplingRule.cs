using System;
using System.Collections.Generic;
using IIMes.Infrastructure.DomainObject;
using Newtonsoft.Json;

namespace IIMes.Services.Core.Model
{
    public class SSamplingRule : DomainObject, IAggregateRoot
    {
        public SSamplingRule()
        {
        }

        [JsonIgnore]
        public virtual SSetting SSetting { get; set; }

        public virtual string Code { get; set; }

        public virtual string Name { get; set; }

        public virtual string Type { get; set; }

        public virtual string Description { get; set; }

        public virtual string Description2 { get; set; }
    }
}
