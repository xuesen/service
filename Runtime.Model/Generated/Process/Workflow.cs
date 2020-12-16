using System;
using IIMes.Infrastructure.DomainObject;
using System.Collections.Generic;

namespace IIMes.Services.Runtime.Model.Process
{
    public partial class Workflow : DomainObject, IAggregateRoot
    {
        public virtual WorkflowBase WorkflowBase { get; set; }

        public virtual string Version { get; set; }

        public virtual string VersionSequence { get; set; }

        public virtual string Status { get; set; }

        public virtual string WorkflowJson { get; set; }

        public virtual string Description { get; set; }
    }
}
