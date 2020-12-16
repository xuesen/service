using System;
using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class SWorkflowStepDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int? Id { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual string Version { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual string VersionSequence { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual string Status { get; set; }

        [ProtoMember(5, DataFormat = DataFormat.WellKnown)]
        public virtual string WorkflowJson { get; set; }

        [ProtoMember(6, DataFormat = DataFormat.WellKnown)]
        public virtual string Description { get; set; }

        [ProtoMember(7, DataFormat = DataFormat.WellKnown)]
        public virtual string Editor { get; set; }

        [ProtoMember(8, DataFormat = DataFormat.WellKnown)]
        public virtual int WorkflowBaseId { get; set; }
    }
}