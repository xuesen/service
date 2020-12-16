using System;
using IIMes.Infrastructure.DomainObject;
using IIMes.Services.Runtime.Model.Plant;
using IIMes.Services.Runtime.Model.Process;
using IIMes.Services.Runtime.Model.Product;
using IIMes.Services.Runtime.Model.System;

namespace IIMes.Services.Runtime.Model.Production
{
    public class WoBase : DomainObject, IAggregateRoot
    {
        public virtual string WorkOrder { get; set; }

        public virtual Part Part { get; set; }

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

        public virtual int? ScrapQty { get; set; }

        public virtual Workflow Workflow { get; set; }

        public virtual int BopId { get; set; }

        public virtual int StatusId { get; set; }

        public virtual WoStatus WoStatus { get; set; }

        public virtual ProductLine ProductLine { get; set; }

        public virtual Process.Process StartProcess { get; set; }

        public virtual Process.Process EndProcess { get; set; }

        public virtual BarcodeRule RcBarcodeRule { get; set; }

        public virtual BarcodeRule SnBarcodeRule { get; set; }

        public virtual string Description { get; set; }

        public virtual int? PrintQty { get; set; }

        public virtual Bom Bom { get; set; }

        public virtual string Po { get; set; }
    }
}
