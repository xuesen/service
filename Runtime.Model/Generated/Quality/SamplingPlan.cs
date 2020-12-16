using IIMes.Infrastructure.DomainObject;
using IIMes.Services.Runtime.Model.System;
using Newtonsoft.Json;

namespace IIMes.Services.Runtime.Model.Quality
{
    public class SamplingPlan : DomainObject, IAggregateRoot
    {
        [JsonIgnore]
        public virtual Setting Setting { get; set; }

        public virtual string Description { get; set; }

        public virtual string Name { get; set; }

        public virtual string Type { get; set; }
    }
}
