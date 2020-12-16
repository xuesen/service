using System;
using IIMes.Services.Core.Model;
using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class SBopProcessSamplingDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int? Id { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual SBopBase BopBase { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual SSamplingPlan SamplingPlan { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual SSamplingRule SamplingRule { get; set; }

        [ProtoMember(5, DataFormat = DataFormat.WellKnown)]
        public virtual int ProcessId { get; set; }

        [ProtoMember(6, DataFormat = DataFormat.WellKnown)]
        public virtual string OqcSamplingType { get; set; }

        [ProtoMember(7, DataFormat = DataFormat.WellKnown)]
        public virtual int? OqcSamplingQty { get; set; }

        [ProtoMember(8, DataFormat = DataFormat.WellKnown)]
        public virtual string Editor { get; set; }
    }
}