using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.Production
{
    public partial class ProductInfoRemove : DomainObject, IAggregateRoot
    {
        public virtual string SerialNumber { get; set; }

        public virtual int ProcessId { get; set; }

        public virtual string ProcessCode { get; set; }

        public virtual int RemoveProcessId { get; set; }

        public virtual string RemoveProcessCode { get; set; }

        public virtual string Type { get; set; }

        public virtual int ItemPartId { get; set; }

        public virtual string ItemPartNo { get; set; }

        public virtual int ScanPartId { get; set; }

        public virtual string ScanPartNo { get; set; }

        public virtual string ScanPartValue { get; set; }

        public virtual string RemoveEmployeeCode { get; set; }

        public virtual string Remark { get; set; }
    }
}
