using System;
using System.Collections.Generic;
using ProtoBuf;

namespace IIMes.Services.Runtime.Interface.DTO
{
    [ProtoContract]
    public class WorRepairInfoDTO
    {
        // 工单
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual string WorkOrder { get; set; }

        // 物料
        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual PartItemDTO Part { get; set; }

        // 不良生产线
        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual string DefectPdLineCode { get; set; }

        // 不良工序
        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual CommonDTO DefectProcess { get; set; }

        // 不良工作站
        [ProtoMember(5, DataFormat = DataFormat.WellKnown)]
        public virtual string DefectTerminalCode { get; set; }

        // 不良现象
        [ProtoMember(6, DataFormat = DataFormat.WellKnown)]
        public virtual IList<WorDefectInfoDTO> DefectList { get; set; }

        // 维修信息
        [ProtoMember(7, DataFormat = DataFormat.WellKnown)]
        public virtual WorRepairItemDTO RepairInfo { get; set; }

        // 维修站工序
        [ProtoMember(8, DataFormat = DataFormat.WellKnown)]
        public virtual CommonDTO RepairProcess { get; set; }

        // 打不良站点logid
        [ProtoMember(9, DataFormat = DataFormat.WellKnown)]
        public virtual int TestProductLogId { get; set; }

        // 编辑人
        [ProtoMember(10, DataFormat = DataFormat.WellKnown)]
        public virtual string Editor { get; set; }
    }
}
