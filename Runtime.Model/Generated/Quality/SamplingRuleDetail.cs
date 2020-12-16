using IIMes.Infrastructure.DomainObject;
using IIMes.Services.Runtime.Model.System;
using Newtonsoft.Json;

namespace IIMes.Services.Runtime.Model.Quality
{
    public class SamplingRuleDetail : DomainObject, IAggregateRoot
    {
        [JsonIgnore]
        public virtual Setting Setting { get; set; }

        public virtual int SamplingRuleId { get; set; }

        public virtual int FromSamplePlanId { get; set; }

        public virtual int ToSamplePlanId { get; set; }

        public virtual int? SerialQty { get; set; }

        public virtual int? FailQty { get; set; }

        public virtual int Action { get; set; }

        [JsonIgnore]
        public virtual SamplingRule SamplingRule { get; set; }

        [JsonIgnore]
        public virtual SamplingPlan SamplingPlanFrom { get; set; }

        [JsonIgnore]
        public virtual SamplingPlan SamplingPlanTo { get; set; }
    }
}