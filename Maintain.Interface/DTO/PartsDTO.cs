using System;
using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class PartsDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int PartId { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual string PartNo { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual string PartName { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual string SPEC1 { get; set; }

        [ProtoMember(5, DataFormat = DataFormat.WellKnown)]
        public virtual string SPEC2 { get; set; }
    }
}
