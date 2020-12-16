using System;
using System.Collections.Generic;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public partial class SPartFs : DomainObject
    {
        public virtual int PartId { get; set; }

        public virtual string Side { get; set; }

        public virtual int EquipmentId { get; set; }

        public virtual string Programe { get; set; }

        public virtual string Version { get; set; }

        public virtual string EmployeeCode { get; set; }

        public virtual DateTime CreateTime { get; set; }

        public virtual ISet<SPartFsDetail> SPartFsDetail { get; set; }
    }
}
