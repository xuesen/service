using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public class SBopProcessSampling : DomainObject, IAggregateRoot
    {
        public virtual SBopBase SBopBase { get; set; }

        public virtual SSamplingPlan SSamplingPlan { get; set; }

        public virtual SSamplingRule SSamplingRule { get; set; }

        public virtual int ProcessId { get; set; }

        public virtual string OqcSamplingType { get; set; }

        public virtual int? OqcSamplingQty { get; set; }
    }
}
