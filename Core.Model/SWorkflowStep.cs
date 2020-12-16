using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public partial class SWorkflowStep : DomainObject, IAggregateRoot
    {
        public virtual SWorkflow SWorkflow { get; set; }

        public virtual SProcess SProcess { get; set; }

        public virtual SProcess SNextProcess { get; set; }

        public virtual SSetting SSetting { get; set; }

        public virtual string Priority { get; set; }

        public virtual string Spname { get; set; }
    }
}
