using System;
using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class SSamplingPlanDetailDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int Id { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual int SamplingPlanId { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual int LotMin { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual int LotMax { get; set; }

        [ProtoMember(5, DataFormat = DataFormat.WellKnown)]
        public virtual int SampleQty { get; set; }

        [ProtoMember(6, DataFormat = DataFormat.WellKnown)]
        public virtual int CriticalQty { get; set; }

        [ProtoMember(7, DataFormat = DataFormat.WellKnown)]
        public virtual int MajorQty { get; set; }

        [ProtoMember(8, DataFormat = DataFormat.WellKnown)]
        public virtual int MinorQty { get; set; }

        [ProtoMember(9, DataFormat = DataFormat.WellKnown)]
        public virtual string Editor { get; set; }

        [ProtoMember(12, DataFormat = DataFormat.WellKnown)]
        public virtual string SamplingPlanName { get; set; }
    }
}