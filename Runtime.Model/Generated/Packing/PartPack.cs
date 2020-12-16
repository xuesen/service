using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.Packing
{
    public partial class PartPack : DomainObject, IAggregateRoot
    {
        public virtual int PartId { get; set; }

        public virtual int? BoxQty { get; set; }

        public virtual int? CartonQty { get; set; }

        public virtual int? PalletQty { get; set; }

        public virtual int? BoxBarcodeRuleId { get; set; }

        public virtual int? CartonBarcodeRuleId { get; set; }

        public virtual int? PalletBarcodeRuleId { get; set; }
    }
}
