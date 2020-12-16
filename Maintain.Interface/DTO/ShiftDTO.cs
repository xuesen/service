using System;
using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class ShiftDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int? Id { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual string Code { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual string Name { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual string StartTime { get; set; }

        [ProtoMember(5, DataFormat = DataFormat.WellKnown)]
        public virtual string EndTime { get; set; }

        [ProtoMember(6, DataFormat = DataFormat.WellKnown)]
        public virtual ShiftGroupDTO ShiftGroup { get; set; }

        [ProtoMember(7, DataFormat = DataFormat.WellKnown)]
        public virtual LayerEditorDTO Editor { get; set; }

        [ProtoMember(8, DataFormat = DataFormat.WellKnown)]
        public virtual DateTime Update_time { get; set; }
    }
}
