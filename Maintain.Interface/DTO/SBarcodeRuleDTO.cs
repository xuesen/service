using System;
using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class SBarcodeRuleDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int Id { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual string RuleName { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual int RuleType { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual string RuleDesc { get; set; }

        [ProtoMember(5, DataFormat = DataFormat.WellKnown)]
        public virtual string SerialJson { get; set; }

        [ProtoMember(6, DataFormat = DataFormat.WellKnown)]
        public virtual string Prefix { get; set; }

        [ProtoMember(7, DataFormat = DataFormat.WellKnown)]
        public virtual string Suffix { get; set; }

        [ProtoMember(8, DataFormat = DataFormat.WellKnown)]
        public virtual string Editor { get; set; }

        [ProtoMember(9, DataFormat = DataFormat.WellKnown)]
        public virtual string Status { get; set; }

        [ProtoMember(12, DataFormat = DataFormat.WellKnown)]
        public virtual string RuleTypeName { get; set; }
    }
}