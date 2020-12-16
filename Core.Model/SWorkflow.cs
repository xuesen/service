using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public partial class SWorkflow : DomainObject, IAggregateRoot
    {
        public virtual SWorkflowBase SWorkflowBase { get; set; }

        public virtual string Version { get; set; }

        public virtual string VersionSequence { get; set; }

        public virtual string Status { get; set; }

        public virtual string WorkflowJson { get; set; }

        public virtual string Description { get; set; }
    }
}
