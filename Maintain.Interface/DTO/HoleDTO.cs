using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class HoleDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual string No { get; set; }
    }
}
