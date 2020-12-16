using System;
using IIMes.Services.Core.Model;
using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class SBopBaseDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int? Id { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual DateTime Lastestedittime { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual string Workflowid { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual string Workflowlastversion { get; set; }

        [ProtoMember(5, DataFormat = DataFormat.WellKnown)]
        public virtual string WorkflowBasename { get; set; }

        [ProtoMember(6, DataFormat = DataFormat.WellKnown)]
        public virtual string Part_id { get; set; }

        [ProtoMember(7, DataFormat = DataFormat.WellKnown)]
        public virtual string Part_no { get; set; }

        [ProtoMember(8, DataFormat = DataFormat.WellKnown)]
        public virtual string Part_name { get; set; }

        [ProtoMember(9, DataFormat = DataFormat.WellKnown)]
        public virtual DateTime? Latestpubtime { get; set; }

        [ProtoMember(10, DataFormat = DataFormat.WellKnown)]
        public virtual string Status { get; set; }

        [ProtoMember(11, DataFormat = DataFormat.WellKnown)]
        public virtual string Family_id { get; set; }

        [ProtoMember(12, DataFormat = DataFormat.WellKnown)]
        public virtual string Family_code { get; set; }

        [ProtoMember(13, DataFormat = DataFormat.WellKnown)]
        public virtual string Family_name { get; set; }

        [ProtoMember(14, DataFormat = DataFormat.WellKnown)]
        public virtual string Editor { get; set; }

        [ProtoMember(15, DataFormat = DataFormat.WellKnown)]
        public virtual SPart Part { get; set; }
    }
}