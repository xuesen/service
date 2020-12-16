using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public class SBopBase : DomainObject, IAggregateRoot
    {
        public virtual SPart SPart { get; set; }

        public virtual SWorkflow SWorkflow { get; set; }

        public virtual string Status { get; set; }

        public virtual string WorkflowVersion { get; set; }

        public virtual SFamily SFamily { get; set; }
    }
}
