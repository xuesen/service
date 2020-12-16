using System;
using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class SSamplingRuleDetailDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int Id { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual int SamplingRuleId { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual int FromSamplePlanId { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual int ToSamplePlanId { get; set; }

        [ProtoMember(5, DataFormat = DataFormat.WellKnown)]
        public virtual int SerialQty { get; set; }

        [ProtoMember(6, DataFormat = DataFormat.WellKnown)]
        public virtual int FailQty { get; set; }

        [ProtoMember(7, DataFormat = DataFormat.WellKnown)]
        public virtual int Action { get; set; }

        [ProtoMember(8, DataFormat = DataFormat.WellKnown)]
        public virtual string Editor { get; set; }

        [ProtoMember(11, DataFormat = DataFormat.WellKnown)]
        public virtual string ActionName { get; set; }

        [ProtoMember(12, DataFormat = DataFormat.WellKnown)]
        public virtual string SamplingRuleName { get; set; }

        [ProtoMember(13, DataFormat = DataFormat.WellKnown)]
        public virtual string FromSamplePlanName { get; set; }

        [ProtoMember(14, DataFormat = DataFormat.WellKnown)]
        public virtual string ToSamplePlanName { get; set; }
    }
}