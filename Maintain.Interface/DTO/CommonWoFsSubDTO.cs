using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class CommonWoFsSubDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int SubPartId { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual string SubPartNo { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual string SubPartName { get; set; }
    }
}
