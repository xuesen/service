using System;
using IIMes.Infrastructure.DomainObject;
using IIMes.Services.Runtime.Model.Product;

namespace IIMes.Services.Runtime.Model.Process
{
    public partial class WorkflowBase : DomainObject, IAggregateRoot
    {
        public virtual Part Part { get; set; }

        public virtual Family Family { get; set; }

        public virtual string Name { get; set; }

        public virtual string LastVersion { get; set; }
    }
}
