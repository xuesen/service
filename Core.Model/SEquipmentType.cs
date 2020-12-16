using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public partial class SEquipmentType : DomainObject, IAggregateRoot
    {
        public virtual SEquipmentCatagory SEquipmentCatagory { get; set; }

        public virtual SDepartment SDepartment { get; set; }

        public virtual string Code { get; set; }

        public virtual string Name { get; set; }

        public virtual string Brand { get; set; }

        public virtual string Description { get; set; }

        public virtual string Description2 { get; set; }
    }
}
