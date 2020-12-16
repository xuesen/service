using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public partial class SWorkflowBase : DomainObject, IAggregateRoot
    {
        public virtual SPart SPart { get; set; }

        public virtual SFamily SFamily { get; set; }

        public virtual string Name { get; set; }

        public virtual string LastVersion { get; set; }
    }
}
