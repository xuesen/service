using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public class WoFs : DomainObject, IAggregateRoot
    {
        public virtual string PartFsId { get; set; }

        public virtual string WorkOrder { get; set; }

        public virtual int PartId { get; set; }

        public virtual string Side { get; set; }

        public virtual int EquipmentId { get; set; }

        public virtual string EmployeeCode { get; set; }

        public virtual DateTime CreateTime { get; set; }

        public virtual DateTime UpdateTime { get; set; }
    }
}
