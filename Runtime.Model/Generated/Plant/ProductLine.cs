using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.Plant
{
    public partial class ProductLine : DomainObject, IAggregateRoot
    {
        public virtual Workshop Workshop { get; set; }

        public virtual int WorkshopId { get; set; }

        public virtual string Code { get; set; }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual string Description2 { get; set; }
    }
}
