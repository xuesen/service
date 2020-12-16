using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.WOR
{
    public partial class WorStatus : DomainObject, IAggregateRoot
    {
        public virtual string WorkOrder { get; set; }

        public virtual int PartId { get; set; }

        public virtual string PartNo { get; set; }

        public virtual int ProductLineId { get; set; }

        public virtual string ProductLineCode { get; set; }

        public virtual int ProcessId { get; set; }

        public virtual string ProcessCode { get; set; }

        public virtual int InputQty { get; set; }

        public virtual int PassQty { get; set; }

        public virtual int FailQty { get; set; }

        public virtual DateTime? ProcessStartTime { get; set; }

        public virtual DateTime? ProcessEndTime { get; set; }
    }
}
