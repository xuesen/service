using System.Collections.Generic;
using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class FeederDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual IList<FeederItem> Feeder { get; set; }
    }
}
