using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class CoreDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual PlaceDTO Place { get; set; }
    }
}
