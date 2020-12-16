using System;
using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class PartMarketDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int PartId { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual string PartNo { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual string PartName { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual string Spec1 { get; set; }
    }
}
