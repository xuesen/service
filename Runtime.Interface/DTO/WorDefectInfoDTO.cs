using System;
using System.Collections.Generic;
using ProtoBuf;

namespace IIMes.Services.Runtime.Interface.DTO
{
    [ProtoContract]
    public class WorDefectInfoDTO
    {
        // 不良结果自增id
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int WorDefectId { get; set; }

        // 不良现象id, 不良现象code, 不良现象描述
        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual CommonDTO Defect { get; set; }

        // 不良现象remark
        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual string DefectRemark { get; set; }

        // 不良类型
        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual string DefectType { get; set; }
    }
}
