using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.Packing
{
    public partial class PackUnit : DomainObject, IAggregateRoot
    {
        public virtual string SerialNumber { get; set; }

        public virtual int? PartId { get; set; }

        public virtual string PartNo { get; set; }

        public virtual string WorkOrder { get; set; }

        public virtual string BoxNo { get; set; }

        public virtual string CartonNo { get; set; }

        public virtual string PalletNo { get; set; }
    }
}
