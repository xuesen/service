using System;
using System.Collections.Generic;
using ProtoBuf;

namespace IIMes.Services.Runtime.Interface.DTO
{
    [ProtoContract]
    public class WorDefectKeypartsSaveRequestDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual string SN { get; set; }

        // 不良关键料SN, 不良物料No, 不良物料Name, 不良物料类型
        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual ScanPartDTO DefectPart { get; set; }

        // 新关键料SN, 新物料No, 新物料Name, 新物料类型
        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual ScanPartDTO NewPart { get; set; }

        // 描述
        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual string Remark { get; set; }

        [ProtoMember(5, DataFormat = DataFormat.WellKnown)]
        public virtual string TerminalCode { get; set; }

        [ProtoMember(6, DataFormat = DataFormat.WellKnown)]
        public virtual string Editor { get; set; }
    }
}
