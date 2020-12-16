using System;
using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class SPartDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int? Id { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual string PartNo { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual string PartName { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual string Spec1 { get; set; }

        [ProtoMember(5, DataFormat = DataFormat.WellKnown)]
        public virtual string Spec2 { get; set; }

        [ProtoMember(6, DataFormat = DataFormat.WellKnown)]
        public virtual string Version { get; set; }

        [ProtoMember(7, DataFormat = DataFormat.WellKnown)]
        public virtual LayerCommonDTO Model { get; set; }

        [ProtoMember(8, DataFormat = DataFormat.WellKnown)]
        public virtual string PartXl { get; set; }

        [ProtoMember(9, DataFormat = DataFormat.WellKnown)]
        public virtual LayerCommonDTO MaterialType { get; set; }

        [ProtoMember(10, DataFormat = DataFormat.WellKnown)]
        public virtual string Uom { get; set; }

        [ProtoMember(11, DataFormat = DataFormat.WellKnown)]
        public virtual string MatchRule { get; set; }

        [ProtoMember(12, DataFormat = DataFormat.WellKnown)]
        public virtual bool UniqueCheck { get; set; }

        [ProtoMember(13, DataFormat = DataFormat.WellKnown)]
        public virtual LayerCommonDTO Vendor { get; set; }

        [ProtoMember(14, DataFormat = DataFormat.WellKnown)]
        public virtual string VendorPartNo { get; set; }

        [ProtoMember(15, DataFormat = DataFormat.WellKnown)]
        public virtual LayerCommonDTO Customer { get; set; }

        [ProtoMember(16, DataFormat = DataFormat.WellKnown)]
        public virtual string CustomerPartNo { get; set; }

        [ProtoMember(17, DataFormat = DataFormat.WellKnown)]
        public virtual string Editor { get; set; }

        [ProtoMember(18, DataFormat = DataFormat.WellKnown)]
        public virtual bool Keypart { get; set; }
    }
}