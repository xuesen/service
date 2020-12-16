using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public class WoBase : DomainObject, IAggregateRoot
    {
        public virtual SPart SPart { get; set; }

        public virtual SWorkflow SWorkflow { get; set; }

        public virtual int StatusId { get; set; }

        public virtual SWoStatus SWoStatus { get; set; }

        public virtual int? ProductLineId { get; set; }

        public virtual SProductLine SProductLine { get; set; }

        public virtual SProcess SProcess { get; set; }

        public virtual SProcess SProcessEnd { get; set; }

        public virtual int? RcBarcodeRuleId { get; set; }

        public virtual SBarcodeRule RcBarcodeRule { get; set; }

        public virtual int? SnBarcodeRuleId { get; set; }

        public virtual SBarcodeRule SnBarcodeRule { get; set; }

        public virtual string WorkOrder { get; set; }

        public virtual string Type { get; set; }

        public virtual string Version { get; set; }

        public virtual int? TargetQty { get; set; }

        public virtual DateTime? WoCreateTime { get; set; }

        public virtual DateTime? WoScheduleTime { get; set; }

        public virtual DateTime? WoScheduleFinishTime { get; set; }

        public virtual int? Sequence { get; set; }

        public virtual DateTime? WoStartTime { get; set; }

        public virtual DateTime? WoCloseTime { get; set; }

        public virtual int? InputQty { get; set; }

        public virtual int? OutputQty { get; set; }

        public virtual string Description { get; set; }

        public virtual int? PrintQty { get; set; }

        public virtual int? BomId { get; set; }

        public virtual SBom SBom { get; set; }

        public virtual int? BopId { get; set; }

        public virtual int? ScrapQty { get; set; }

        public virtual string Po { get; set; }
    }
}
