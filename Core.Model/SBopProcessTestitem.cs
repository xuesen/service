using IIMes.Infrastructure.DomainObject;
using Newtonsoft.Json;

namespace IIMes.Services.Core.Model
{
    public class SBopProcessTestitem : DomainObject, IAggregateRoot
    {
        [JsonIgnore]
        public virtual int BopBaseId { get; set; }

        public virtual int ProcessId { get; set; }

        public virtual STestItem STestItem { get; set; }

        public virtual decimal? Sl { get; set; }

        public virtual decimal? Usl { get; set; }

        public virtual decimal? Lsl { get; set; }

        public virtual decimal? Cl { get; set; }

        public virtual decimal? Ucl { get; set; }

        public virtual decimal? Lcl { get; set; }

        public virtual string Unit { get; set; }

        public virtual int Sequence { get; set; }
    }
}
