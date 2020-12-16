using System;
using System.Collections.Generic;
using IIMes.Infrastructure.DomainObject;
using Newtonsoft.Json;

namespace IIMes.Services.Core.Model
{
    public class SSamplingPlan : DomainObject, IAggregateRoot
    {
        public SSamplingPlan()
        {
        }

        [JsonIgnore]
        public virtual SSetting SSetting { get; set; }

        public virtual string Description { get; set; }

        public virtual string Name { get; set; }

        public virtual string Type { get; set; }
    }
}
