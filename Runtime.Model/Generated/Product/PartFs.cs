using System;
using System.Collections.Generic;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.Product
{
    public partial class PartFs : DomainObject
    {
        public virtual int PartId { get; set; }

        public virtual string Side { get; set; }

        public virtual int EquipmentId { get; set; }

        public virtual string Programe { get; set; }

        public virtual string Version { get; set; }

        public virtual ISet<PartFsDetail> PartFsDetail { get; set; }
    }
}
