using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class ProgramItem
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual CoreDTO Core { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual MachineDTO Machine { get; set; }
    }
}
