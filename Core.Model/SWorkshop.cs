using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public partial class SWorkshop : DomainObject, IAggregateRoot
    {
        public virtual SFactory SFactory { get; set; }

        public virtual int FactoryId { get; set; }

        public virtual string Code { get; set; }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual string Description2 { get; set; }
    }
}
