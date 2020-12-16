using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class FeederItem
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual HoleDTO HoleNo { get; set; }

        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual string CompoName { get; set; }

        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual string FeederType { get; set; }
    }
}
