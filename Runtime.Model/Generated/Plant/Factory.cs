using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.Plant
{
    public partial class Factory : DomainObject, IAggregateRoot
    {
        public virtual string Code { get; set; }

        public virtual string Name { get; set; }

        public virtual string Group { get; set; }

        public virtual string Description { get; set; }

        public virtual string Description2 { get; set; }
    }
}
