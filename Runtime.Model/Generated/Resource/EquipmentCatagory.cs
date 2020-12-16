using System;
using IIMes.Infrastructure.DomainObject;
using IIMes.Services.Runtime.Model.System;

namespace IIMes.Services.Runtime.Model.Resource
{
    public partial class EquipmentCatagory : DomainObject, IAggregateRoot
    {
        public virtual Setting Setting { get; set; }

        public virtual string Code { get; set; }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual string Description2 { get; set; }
    }
}
