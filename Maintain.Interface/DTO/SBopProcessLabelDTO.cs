using System;
using IIMes.Services.Core.Model;
using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class SBopProcessLabelDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int? Id { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual int BopBaseId { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual int ProcessId { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual string SpecialCheck { get; set; }

        [ProtoMember(5, DataFormat = DataFormat.WellKnown)]
        public virtual SPrintTemplate PrintTemplate { get; set; }

        [ProtoMember(6, DataFormat = DataFormat.WellKnown)]
        public virtual int Piece { get; set; }

        [ProtoMember(7, DataFormat = DataFormat.WellKnown)]
        public virtual int TriggerId { get; set; }

        [ProtoMember(8, DataFormat = DataFormat.WellKnown)]
        public virtual int Priority { get; set; }

        [ProtoMember(9, DataFormat = DataFormat.WellKnown)]
        public virtual string Editor { get; set; }

        [ProtoMember(10, DataFormat = DataFormat.WellKnown)]
        public virtual string TriggerName { get; set; }
    }
}