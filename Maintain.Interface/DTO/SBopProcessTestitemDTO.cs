using System;
using IIMes.Services.Core.Model;
using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class SBopProcessTestitemDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int? Id { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual int BopBaseId { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual int ProcessId { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual STestItemDTO TestItem { get; set; }

        [ProtoMember(5, DataFormat = DataFormat.WellKnown)]
        public virtual decimal? Sl { get; set; }

        [ProtoMember(6, DataFormat = DataFormat.WellKnown)]
        public virtual decimal? Usl { get; set; }

        [ProtoMember(7, DataFormat = DataFormat.WellKnown)]
        public virtual decimal? Lsl { get; set; }

        [ProtoMember(8, DataFormat = DataFormat.WellKnown)]
        public virtual decimal? Cl { get; set; }

        [ProtoMember(9, DataFormat = DataFormat.WellKnown)]
        public virtual decimal? Ucl { get; set; }

        [ProtoMember(10, DataFormat = DataFormat.WellKnown)]
        public virtual decimal? Lcl { get; set; }

        [ProtoMember(11, DataFormat = DataFormat.WellKnown)]
        public virtual string Unit { get; set; }

        [ProtoMember(12, DataFormat = DataFormat.WellKnown)]
        public virtual int? Sequence { get; set; }

        [ProtoMember(13, DataFormat = DataFormat.WellKnown)]
        public virtual string Editor { get; set; }
    }
}