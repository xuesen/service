using System;
using System.Collections.Generic;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.Product
{
    public partial class WoFs : DomainObject
    {
        public virtual string PartFsId { get; set; }

        public virtual string WorkOrder { get; set; }

        public virtual int PartId { get; set; }

        public virtual string Side { get; set; }

        public virtual int EquipmentId { get; set; }
    }
}
