using System;
using IIMes.Services.Core.Model;
using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class SPartPackDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int? Id { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual int PartId { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual int? BoxQty { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual int? CartonQty { get; set; }

        [ProtoMember(5, DataFormat = DataFormat.WellKnown)]
        public virtual int? PalletQty { get; set; }

        [ProtoMember(6, DataFormat = DataFormat.WellKnown)]
        public virtual SBarcodeRule BoxBarcodeRule { get; set; }

        [ProtoMember(7, DataFormat = DataFormat.WellKnown)]
        public virtual SBarcodeRule CartonBarcodeRule { get; set; }

        [ProtoMember(8, DataFormat = DataFormat.WellKnown)]
        public virtual SBarcodeRule PalletBarcodeRule { get; set; }

        [ProtoMember(12, DataFormat = DataFormat.WellKnown)]
        public virtual string Editor { get; set; }
    }
}