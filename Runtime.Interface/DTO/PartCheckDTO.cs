using System;
using System.Collections.Generic;
using IIMes.Services.Runtime.Model;
using ProtoBuf;

namespace IIMes.Services.Runtime.Interface.DTO
{
    public class PartCheckDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int PartId { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual string PartNo { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual string PartName { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual string SPEC1 { get; set; }

        [ProtoMember(5, DataFormat = DataFormat.WellKnown)]
        public virtual string MatchRule { get; set; }

        [ProtoMember(6, DataFormat = DataFormat.WellKnown)]
        public virtual decimal Qty { get; set; }

        [ProtoMember(7, DataFormat = DataFormat.WellKnown)]
        public virtual IList<string> PartSNs { get; set; }

    }
}