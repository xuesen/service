using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public class SBop : DomainObject, IAggregateRoot
    {
        public virtual int? PartId { get; set; }

        public virtual int FamilyId { get; set; }

        public virtual int WorkflowId { get; set; }

        public virtual string WorkflowVersion { get; set; }

        public virtual string Content { get; set; }

        public virtual DateTime? EffectiveDate { get; set; }
    }
}
