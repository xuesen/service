using System;
using System.Collections.Generic;
using IIMes.Services.Runtime.Model;
using ProtoBuf;

namespace IIMes.Services.Runtime.Interface.DTO
{
    public class WorRepairItemInfoDTO
    {
        // 工单
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual string WorkOrder { get; set; }

        // 不良板卡序号
        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual string SN { get; set; }

        // 不良现象
        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual CommonDTO Symptom { get; set; }

        // 不良原因
        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual CommonDTO Cause { get; set; }    

        // 责任类别
        [ProtoMember(5, DataFormat = DataFormat.WellKnown)]
        public virtual CommonDTO Duty { get; set; } 

        // 位置
        [ProtoMember(6, DataFormat = DataFormat.WellKnown)]
        public virtual string Location { get; set; }

        // 维修方式
        [ProtoMember(7, DataFormat = DataFormat.WellKnown)]
        public virtual CommonDTO RepairType { get; set; }

        // 描述
        [ProtoMember(8, DataFormat = DataFormat.WellKnown)]
        public virtual string Description { get; set; }

        // 维修工序
        [ProtoMember(9, DataFormat = DataFormat.WellKnown)]
        public virtual CommonDTO RepairProcess { get; set; }

        // 打不良站Log Id
        [ProtoMember(10, DataFormat = DataFormat.WellKnown)]
        public virtual int TestWorLogId { get; set; }

        // 工作站
        [ProtoMember(11, DataFormat = DataFormat.WellKnown)]
        public virtual string TerminalCode { get; set; }

        // repairlogdetail表自增id
        [ProtoMember(12, DataFormat = DataFormat.WellKnown)]
        public virtual int RepairLogDetailId { get; set; }

        [ProtoMember(13, DataFormat = DataFormat.WellKnown)]
        public virtual string Editor { get; set; }
    }
}