using System;
using IIMes.Infrastructure.DomainObject;
using Newtonsoft.Json;

namespace IIMes.Services.Core.Model
{
    public class SSamplingRuleDetail : DomainObject, IAggregateRoot
    {
        [JsonIgnore]
        public virtual SSetting SSetting { get; set; }

        public virtual int SamplingRuleId { get; set; }

        public virtual int FromSamplePlanId { get; set; }

        public virtual int ToSamplePlanId { get; set; }

        public virtual int? SerialQty { get; set; }

        public virtual int? FailQty { get; set; }

        public virtual int Action { get; set; }

        [JsonIgnore]
        public virtual SSamplingRule SSamplingRule { get; set; }

        [JsonIgnore]
        public virtual SSamplingPlan SSamplingPlanFrom { get; set; }

        [JsonIgnore]
        public virtual SSamplingPlan SSamplingPlanTo { get; set; }
    }
}