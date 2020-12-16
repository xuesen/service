using System;
using System.Collections.Generic;
using ProtoBuf;

namespace IIMes.Services.Runtime.Interface.DTO
{
    [ProtoContract]
    public class WorRepairFinishRequestDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual string SN { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual IList<int> DefectIdList { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual int RepairLogId { get; set; }       

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual string TerminalCode { get; set; }

        [ProtoMember(5, DataFormat = DataFormat.WellKnown)]
        public virtual string Editor { get; set; }
    }
}
