using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class MachineDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual FeederDTO Feeder { get; set; }
    }
}
