using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public partial class ProductInfo : DomainObject, IAggregateRoot
    {
        public virtual string SerialNumber { get; set; }

        public virtual int ProcessId { get; set; }

        public virtual string ProcessCode { get; set; }

        public virtual string Type { get; set; }

        public virtual int ItemPartId { get; set; }

        public virtual string ItemPartNo { get; set; }

        public virtual int ScanPartId { get; set; }

        public virtual string ScanPartNo { get; set; }

        public virtual string ScanPartValue { get; set; }

        public virtual string EmployeeCode { get; set; }

        public virtual DateTime CreateTime { get; set; }

        public virtual DateTime UpdateTime { get; set; }
    }
}
