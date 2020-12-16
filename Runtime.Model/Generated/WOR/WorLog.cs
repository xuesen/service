using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.WOR
{
    public partial class WorLog : DomainObject, IAggregateRoot
    {
        public virtual string WorkOrder { get; set; }

        public virtual string SerialNumber { get; set; }

        public virtual int PartId { get; set; }

        public virtual string PartNo { get; set; }

        public virtual int ProductLineId { get; set; }

        public virtual string ProductLineCode { get; set; }

        public virtual int ProcessId { get; set; }

        public virtual string ProcessCode { get; set; }

        public virtual int TerminalId { get; set; }

        public virtual string TerminalCode { get; set; }

        public virtual int Qty { get; set; }

        public virtual string Status { get; set; }

        public virtual int Reflow { get; set; }

        public virtual string Remark { get; set; }
    }
}
