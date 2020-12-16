using System;
using System.Collections.Generic;
using ProtoBuf;

namespace IIMes.Services.Runtime.Interface.DTO
{
    [ProtoContract]
    public class WorDefectRequestDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual string WorkOrder { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual string SN { get; set; }

        // 不良现象List
        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual IList<CommonDTO> DefectLst { get; set; }

        // 维修站工序
        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual CommonDTO RepairProcess { get; set; }

        // 打不良站点logid
        [ProtoMember(5, DataFormat = DataFormat.WellKnown)]
        public virtual int TestProductLogId { get; set; }

        [ProtoMember(6, DataFormat = DataFormat.WellKnown)]
        public virtual string Editor { get; set; }
    }
}
