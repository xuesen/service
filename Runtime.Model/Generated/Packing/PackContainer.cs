using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.Packing
{
    public partial class PackContainer : DomainObject, IAggregateRoot
    {
        public virtual int? PartId { get; set; }

        public virtual string PartNo { get; set; }

        public virtual string WorkOrder { get; set; }

        public virtual string ContainerNo { get; set; }

        public virtual string ParentNo { get; set; }

        public virtual string Type { get; set; }

        public virtual string Status { get; set; }

        public virtual int FullQty { get; set; }

        public virtual int PackQty { get; set; }

        public virtual int? TerminalId { get; set; }

        public virtual string TerminalCode { get; set; }

        public virtual string ShipRev { get; set; }
    }
}
