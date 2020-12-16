using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    public class BopProcessRequestDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int BopBaseId { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual int ProcessId { get; set; }
    }
}
