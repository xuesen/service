using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class PlaceItem
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual string PlaceId { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual string CompoName { get; set; }
    }
}
