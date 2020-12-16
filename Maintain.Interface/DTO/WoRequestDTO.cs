using System;
using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class WoRequestDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual string WorkOrder { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual string WoStatus { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual string PartNo { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual string WoScheduleTime { get; set; }

        [ProtoMember(5, DataFormat = DataFormat.WellKnown)]
        public virtual string PdLineCode { get; set; }
    }
}
