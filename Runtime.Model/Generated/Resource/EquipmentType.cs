using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.Resource
{
    public partial class EquipmentType : DomainObject, IAggregateRoot
    {
        public virtual EquipmentCatagory EquipmentCatagory { get; set; }

        public virtual Department Department { get; set; }

        public virtual string Code { get; set; }

        public virtual string Name { get; set; }

        public virtual string Brand { get; set; }

        public virtual string Description { get; set; }

        public virtual string Description2 { get; set; }
    }
}
