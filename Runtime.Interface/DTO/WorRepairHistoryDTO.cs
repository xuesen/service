using System;
using System.Collections.Generic;
using ProtoBuf;

namespace IIMes.Services.Runtime.Interface.DTO
{
    [ProtoContract]
    public class WorRepairHistoryDTO
    {
        // 不良工序
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual string DefectProcessCode { get; set; }

        // 不良时间
        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual string DefectTime { get; set; }

        // 作业员
        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual string Tester { get; set; }

        // 维修时间 
        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual string RepairTime { get; set; }

        // 维修人员
        [ProtoMember(5, DataFormat = DataFormat.WellKnown)]
        public virtual string Editor { get; set; }
    }
}
