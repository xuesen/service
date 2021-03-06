using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public partial class PackUnit : DomainObject, IAggregateRoot
    {
        public virtual string SerialNumber { get; set; }

        public virtual int? PartId { get; set; }

        public virtual string PartNo { get; set; }

        public virtual string WorkOrder { get; set; }

        public virtual string BoxNo { get; set; }

        public virtual string CartonNo { get; set; }

        public virtual string PalletNo { get; set; }

        public virtual string EmployeeCode { get; set; }

        public virtual DateTime CreateTime { get; set; }

        public virtual DateTime UpdateTime { get; set; }
    }
}
