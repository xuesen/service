using System;
using System.Collections.Generic;
using IIMes.Services.Runtime.Model;
using ProtoBuf;

namespace IIMes.Services.Runtime.Interface.DTO
{
    public class ScanPartDTO
    {
        // 关键料SN  SCAN_PART_VALUE
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual string PartSn { get; set; }

        // 物料Id
        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual int PartId { get; set; }

        // 物料No ITEM_PART_NO/SCAN_PART_NO
        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual string PartNo { get; set; }

        // 物料Name PART_NAME
        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual string PartName { get; set; }

        // 物料类型
        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual string PartType { get; set; }

        // 物料规格
        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual string PartSPEC1 { get; set; }
    }
}