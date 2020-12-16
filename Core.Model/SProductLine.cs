using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public partial class SProductLine : DomainObject, IAggregateRoot
    {
        public virtual SWorkshop SWorkshop { get; set; }

        public virtual int WorkshopId { get; set; }

        public virtual string Code { get; set; }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual string Description2 { get; set; }
    }
}
