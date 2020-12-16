using System;
using IIMes.Infrastructure.DomainObject;
using IIMes.Services.Runtime.Model.Quality;

namespace IIMes.Services.Runtime.Model
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

        public virtual SamplingRule SamplingRule { get; set; }

        public virtual SamplingPlan SamplingPlan { get; set; }
    }
}
