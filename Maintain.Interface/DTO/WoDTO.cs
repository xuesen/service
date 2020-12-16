using System;
using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class WoDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int? Id { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual string WorkOrder { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual string Type { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual int PdLineId { get; set; }

        [ProtoMember(5, DataFormat = DataFormat.WellKnown)]
        public virtual string PdLineCode { get; set; }

        [ProtoMember(6, DataFormat = DataFormat.WellKnown)]
        public virtual string PdLineName { get; set; }

        [ProtoMember(7, DataFormat = DataFormat.WellKnown)]
        public virtual int PartId { get; set; }

        [ProtoMember(8, DataFormat = DataFormat.WellKnown)]
        public virtual string PartNo { get; set; }

        [ProtoMember(9, DataFormat = DataFormat.WellKnown)]
        public virtual string PartName { get; set; }

        [ProtoMember(10, DataFormat = DataFormat.WellKnown)]
        public virtual int WoStatusId { get; set; }

        [ProtoMember(10, DataFormat = DataFormat.WellKnown)]
        public virtual string WoStatusCode { get; set; }

        [ProtoMember(10, DataFormat = DataFormat.WellKnown)]
        public virtual string WoStatusName { get; set; }

        [ProtoMember(11, DataFormat = DataFormat.WellKnown)]
        public virtual int TargetQty { get; set; }

        [ProtoMember(12, DataFormat = DataFormat.WellKnown)]
        public virtual int InputQty { get; set; }

        [ProtoMember(13, DataFormat = DataFormat.WellKnown)]
        public virtual int OutPutQty { get; set; }

        [ProtoMember(14, DataFormat = DataFormat.WellKnown)]
        public virtual string WoScheduleTime { get; set; }

        [ProtoMember(15, DataFormat = DataFormat.WellKnown)]
        public virtual string WoScheduleFinishTime { get; set; }

        [ProtoMember(16, DataFormat = DataFormat.WellKnown)]
        public virtual int Sequence { get; set; }

        [ProtoMember(17, DataFormat = DataFormat.WellKnown)]
        public virtual int RcBarcodeRuleId { get; set; }

        [ProtoMember(18, DataFormat = DataFormat.WellKnown)]
        public virtual string RcBarcodeRule { get; set; }

        [ProtoMember(19, DataFormat = DataFormat.WellKnown)]
        public virtual int SnBarcodeRuleId { get; set; }

        [ProtoMember(20, DataFormat = DataFormat.WellKnown)]
        public virtual string SnBarcodeRule { get; set; }

        [ProtoMember(21, DataFormat = DataFormat.WellKnown)]
        public virtual string WoCreateTime { get; set; }

        [ProtoMember(22, DataFormat = DataFormat.WellKnown)]
        public virtual string WoUpdateTime { get; set; }

        [ProtoMember(23, DataFormat = DataFormat.WellKnown)]
        public virtual string Po { get; set; }

        [ProtoMember(24, DataFormat = DataFormat.WellKnown)]
        public virtual string Description { get; set; }

        [ProtoMember(25, DataFormat = DataFormat.WellKnown)]
        public virtual string Update_time { get; set; }

        [ProtoMember(26, DataFormat = DataFormat.WellKnown)]
        public virtual LayerEditorDTO Editor { get; set; }
    }
}
