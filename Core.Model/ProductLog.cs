using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public class ProductLog : DomainObject, IAggregateRoot
    {
        public virtual string SerialNumber { get; set; }

        public virtual int? PartId { get; set; }

        public virtual string PartNo { get; set; }

        public virtual string WorkOrder { get; set; }

        public virtual int WorkflowId { get; set; }

        public virtual int? ProcessId { get; set; }

        public virtual string ProcessCode { get; set; }

        public virtual int? TerminalId { get; set; }

        public virtual string TerminalCode { get; set; }

        public virtual string Ispass { get; set; }

        public virtual DateTime? ProcessStarttime { get; set; }

        public virtual DateTime? ProcessEndttime { get; set; }

        public virtual DateTime? ProductStarttime { get; set; }

        public virtual DateTime? ProductEndtime { get; set; }

        public virtual int? NextProcessId { get; set; }

        public virtual string NextProcessCode { get; set; }

        public virtual string EmployeeCode { get; set; }

        public virtual DateTime CreateTime { get; set; }

        public virtual DateTime UpdateTime { get; set; }
    }
}
