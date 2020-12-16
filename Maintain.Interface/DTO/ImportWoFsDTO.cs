using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class ImportWoFsDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual string WorkOrder { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual int Qty { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual int PartId { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual string PartNo { get; set; }

        [ProtoMember(6, DataFormat = DataFormat.WellKnown)]
        public virtual int PdLineId { get; set; }

        [ProtoMember(7, DataFormat = DataFormat.WellKnown)]
        public virtual string PdLineCode { get; set; }

        [ProtoMember(8, DataFormat = DataFormat.WellKnown)]
        public virtual int EquipmentId { get; set; }

        [ProtoMember(9, DataFormat = DataFormat.WellKnown)]
        public virtual string EquipmentCode { get; set; }

        [ProtoMember(10, DataFormat = DataFormat.WellKnown)]
        public virtual string Side { get; set; }

        [ProtoMember(11, DataFormat = DataFormat.WellKnown)]
        public virtual string EmployeeCode { get; set; }
    }
}
