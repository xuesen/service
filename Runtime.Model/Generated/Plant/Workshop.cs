using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.Plant
{
    public partial class Workshop : DomainObject, IAggregateRoot
    {
        public virtual Factory Factory { get; set; }

        public virtual int FactoryId { get; set; }

        public virtual string Code { get; set; }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual string Description2 { get; set; }
    }
}
