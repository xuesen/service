using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public class QcSampleStatus : DomainObject, IAggregateRoot
    {
        public virtual int BopBaseId { get; set; }

        public virtual string ProcessCode { get; set; }

        public virtual string PartNo { get; set; }

        public virtual int ProcessId { get; set; }

        public virtual int PartId { get; set; }

        public virtual int SampleRuleId { get; set; }

        public virtual int SamplePlanId { get; set; }

        public virtual string SampleStatus { get; set; }

        public virtual string Cache { get; set; }

        public virtual bool Enabled { get; set; }

        public virtual string OqcSamplingType { get; set; }

        public virtual int? OqcSamplingQty { get; set; }

        public virtual DateTime CreateTime { get; set; }

        public virtual DateTime UpdateTime { get; set; }

        public virtual SSamplingRule SamplingRule { get; set; }

        public virtual SSamplingPlan SamplingPlan { get; set; }
    }
}
