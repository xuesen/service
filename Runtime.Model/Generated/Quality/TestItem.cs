using IIMes.Infrastructure.DomainObject;
using IIMes.Services.Runtime.Model.System;
using Newtonsoft.Json;

namespace IIMes.Services.Runtime.Model.Quality
{
    public class TestItem : DomainObject, IAggregateRoot
    {
        [JsonIgnore]
        public virtual TestItemType TestItemType { get; set; }

        [JsonIgnore]
        public virtual Setting Setting { get; set; }

        public virtual string Code { get; set; }

        public virtual string Name { get; set; }

        public virtual string Ratio { get; set; }

        public virtual string Point { get; set; }

        public virtual string Description { get; set; }

        public virtual string Description2 { get; set; }
    }
}
