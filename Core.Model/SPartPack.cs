using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public partial class SPartPack : DomainObject, IAggregateRoot
    {
        public virtual int PartId { get; set; }

        public virtual int? BoxQty { get; set; }

        public virtual int? CartonQty { get; set; }

        public virtual int? PalletQty { get; set; }

        public virtual SBarcodeRule BoxBarcodeRule { get; set; }

        public virtual SBarcodeRule CartonBarcodeRule { get; set; }

        public virtual SBarcodeRule PalletBarcodeRule { get; set; }
    }
}
