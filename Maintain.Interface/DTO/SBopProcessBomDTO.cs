using System;
using IIMes.Services.Core.Model;
using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class SBopProcessBomDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int? Id { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual SBopBase BopBase { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual SProcess Process { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual SPart ItemPart { get; set; }

        [ProtoMember(5, DataFormat = DataFormat.WellKnown)]
        public virtual int Qty { get; set; }

        [ProtoMember(6, DataFormat = DataFormat.WellKnown)]
        public virtual string Location { get; set; }

        [ProtoMember(7, DataFormat = DataFormat.WellKnown)]
        public virtual string Editor { get; set; }
    }
}