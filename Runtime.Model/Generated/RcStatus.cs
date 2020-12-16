using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model
{
    public partial class RcStatus : DomainObject
    {
        public virtual string RcNo { get; set; }

        public virtual string WorkOrder { get; set; }

        public virtual int? WorkflowId { get; set; }

        public virtual int PartId { get; set; }

        public virtual string PartNo { get; set; }

        public virtual int Qty { get; set; }

        public virtual int ProductLineId { get; set; }

        public virtual string ProductLineCode { get; set; }

        public virtual int? ProcessId { get; set; }

        public virtual string ProcessCode { get; set; }

        public virtual int? NextProcessId { get; set; }

        public virtual string NextProcessCode { get; set; }

        public virtual int? TerminalId { get; set; }

        public virtual string TerminalCode { get; set; }

        public virtual DateTime? ProcessStarttime { get; set; }

        public virtual DateTime? ProcessEndttime { get; set; }

        public virtual DateTime? RcStarttime { get; set; }

        public virtual DateTime? RcEndtime { get; set; }

        public virtual string RcStatusVal { get; set; }
    }
}
