using System;
using ProtoBuf;

namespace IIMes.Services.Runtime.Interface.DTO
{
    [ProtoContract]
    public class WorDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual string WorkOrder { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual string PartFs { get; set; }

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
