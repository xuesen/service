using IIMes.Infrastructure.DomainObject;
using IIMes.Services.Runtime.Model.Quality;

namespace IIMes.Services.Runtime.Model.Process
{
    public class BopProcessSampling : DomainObject, IAggregateRoot
    {
        public virtual BopBase BopBase { get; set; }

        public virtual SamplingPlan SamplingPlan { get; set; }

        public virtual SamplingRule SamplingRule { get; set; }

        public virtual int ProcessId { get; set; }

        public virtual string OqcSamplingType { get; set; }

        public virtual int? OqcSamplingQty { get; set; }
    }
}
