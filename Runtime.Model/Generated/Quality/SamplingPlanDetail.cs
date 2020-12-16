using IIMes.Infrastructure.DomainObject;
using Newtonsoft.Json;

namespace IIMes.Services.Runtime.Model.Quality
{
    public class SamplingPlanDetail : DomainObject, IAggregateRoot
    {
        [JsonIgnore]
        public virtual SamplingPlan SamplingPlan { get; set; }

        public virtual int SamplingPlanId { get; set; }

        public virtual int LotMin { get; set; }

        public virtual int LotMax { get; set; }

        public virtual int SampleQty { get; set; }

        public virtual int CriticalQty { get; set; }

        public virtual int MajorQty { get; set; }

        public virtual int MinorQty { get; set; }
    }
}
