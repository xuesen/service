using System;
using IIMes.Infrastructure.DomainObject;
using Newtonsoft.Json;

namespace IIMes.Services.Core.Model
{
    public class SSymptom : DomainObject, IAggregateRoot
    {
        public virtual SSymptomType SSymptomType { get; set; }

        [JsonIgnore]
        public virtual SSetting SSetting { get; set; }

        public virtual string Code { get; set; }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual string Description2 { get; set; }
    }
}