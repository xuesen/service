using System;
using IIMes.Infrastructure.DomainObject;
using IIMes.Services.Runtime.Model.System;

namespace IIMes.Services.Runtime.Model.Process
{
    public partial class WorkflowStep : DomainObject, IAggregateRoot
    {
        public virtual Workflow Workflow { get; set; }

        public virtual Process Process { get; set; }

        public virtual Process NextProcess { get; set; }

        public virtual Setting Setting { get; set; }

        public virtual DateTime CreateTime { get; set; }

        public virtual DateTime UpdateTime { get; set; }

        public virtual string Priority { get; set; }

        public virtual string Spname { get; set; }
    }
}
