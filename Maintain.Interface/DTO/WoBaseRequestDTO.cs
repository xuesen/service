using System;
using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class WoBaseRequestDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual string WorkOrder { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual int PartId { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual int PdLineId { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual string Type { get; set; }

        [ProtoMember(5, DataFormat = DataFormat.WellKnown)]
        public virtual int TargetQty { get; set; }

        [ProtoMember(6, DataFormat = DataFormat.WellKnown)]
        public virtual DateTime WoCreateTime { get; set; }

        [ProtoMember(7, DataFormat = DataFormat.WellKnown)]
        public virtual DateTime WoScheduleTime { get; set; }

        [ProtoMember(8, DataFormat = DataFormat.WellKnown)]
        public virtual DateTime WoScheduleFinishTime { get; set; }

        [ProtoMember(9, DataFormat = DataFormat.WellKnown)]
        public virtual DateTime WoStartTime { get; set; }

        [ProtoMember(10, DataFormat = DataFormat.WellKnown)]
        public virtual DateTime WoCloseTime { get; set; }

        [ProtoMember(11, DataFormat = DataFormat.WellKnown)]
        public virtual int InputQty { get; set; }

        [ProtoMember(12, DataFormat = DataFormat.WellKnown)]
        public virtual int OutputQty { get; set; }

        [ProtoMember(13, DataFormat = DataFormat.WellKnown)]
        public virtual int ScrapQty { get; set; }

        [ProtoMember(14, DataFormat = DataFormat.WellKnown)]
        public virtual int StatusId { get; set; }

        [ProtoMember(15, DataFormat = DataFormat.WellKnown)]
        public virtual string Po { get; set; }

        // modify ITC-1755-0180
        [ProtoMember(16, DataFormat = DataFormat.WellKnown)]
        public virtual int? SnBarcodeRuleId { get; set; }

        [ProtoMember(17, DataFormat = DataFormat.WellKnown)]
        public virtual int RcBarcodeRuleId { get; set; }

        [ProtoMember(18, DataFormat = DataFormat.WellKnown)]
        public virtual string Description { get; set; }

        [ProtoMember(19, DataFormat = DataFormat.WellKnown)]
        public virtual bool Modify { get; set; }

        [ProtoMember(20, DataFormat = DataFormat.WellKnown)]
        public virtual string Editor { get; set; }

        [ProtoMember(21, DataFormat = DataFormat.WellKnown)]
        public virtual int Id { get; set; }
    }
}
