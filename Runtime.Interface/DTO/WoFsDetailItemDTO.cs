using System;
using ProtoBuf;

namespace IIMes.Services.Runtime.Interface.DTO
{
    [ProtoContract]
    public class WoFsDetailItemDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int WoFsDetailId { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual int ItemPartId { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual int ItemPartCount { get; set; }

        // [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        // public virtual string WorkOrder { get; set; }

        // [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        // public virtual string WorkOrder { get; set; }

        // [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        // public virtual string WorkOrder { get; set; }

        // [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        // public virtual string WorkOrder { get; set; }
    }
}
