using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model
{
    public class QcLotResult : DomainObject, IAggregateRoot
    {
        public virtual string LotNubmber { get; set; }

        public virtual int PartId { get; set; }

        public virtual string PartNo { get; set; }

        public virtual string Result { get; set; }

        public virtual int CriticalQty { get; set; }

        public virtual int MajorQty { get; set; }

        public virtual int MinorQty { get; set; }

        public virtual int LotQty { get; set; }

        public virtual int FailQty { get; set; }

        public virtual int SampleQty { get; set; }

        public virtual int PresetSamplePlanId { get; set; }

        public virtual int ExecuteSamplePlanId { get; set; }

        public virtual string SampleLevel { get; set; }

        public virtual int WorkflowId { get; set; }

        public virtual string ProcessCode { get; set; }

        public virtual int TerminalId { get; set; }

        public virtual string TerminalCode { get; set; }
    }
}
