using System;
using System.Collections.Generic;
using IIMes.Services.Runtime.Model;
using IIMes.Services.Runtime.Model.Process;
using ProtoBuf;

namespace IIMes.Services.Runtime.Interface.DTO
{
    public class WoDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual string WorkOrder { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual int TargetQty { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual int PartId { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual string PartNo { get; set; }

        [ProtoMember(5, DataFormat = DataFormat.WellKnown)]
        public virtual string PartName { get; set; }

        [ProtoMember(6, DataFormat = DataFormat.WellKnown)]
        public virtual DateTime WoScheduleTime { get; set; }

        [ProtoMember(7, DataFormat = DataFormat.WellKnown)]
        public virtual int? Sequence { get; set; }

        [ProtoMember(8, DataFormat = DataFormat.WellKnown)]
        public virtual int? ScrapQty { get; set; }

        [ProtoMember(9, DataFormat = DataFormat.WellKnown)]
        public virtual  int? WorkflowId { get; set; }
    }
}