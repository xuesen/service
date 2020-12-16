using System;
using System.Collections.Generic;
using ProtoBuf;

namespace IIMes.Services.Runtime.Interface.DTO
{
    [ProtoContract]
    public class WorRequestDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual string WorkOrder { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual string TerminalCode { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual int InputPassQty { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual int InputFailQty { get; set; }

        [ProtoMember(5, DataFormat = DataFormat.WellKnown)]
        public virtual bool IsReflow { get; set; }

        [ProtoMember(6, DataFormat = DataFormat.WellKnown)]
        public virtual bool IsFirstProcess { get; set; }

        [ProtoMember(7, DataFormat = DataFormat.WellKnown)]
        public virtual string Remark { get; set; }

        [ProtoMember(8, DataFormat = DataFormat.WellKnown)]
        public virtual string SN { get; set; }

        [ProtoMember(9, DataFormat = DataFormat.WellKnown)]
        public virtual IList<CommonDTO> SymptomLst { get; set; }

        [ProtoMember(10, DataFormat = DataFormat.WellKnown)]
        public virtual string Editor { get; set; }
    }
}
