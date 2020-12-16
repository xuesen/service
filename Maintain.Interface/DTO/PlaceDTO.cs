using System.Collections.Generic;
using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class PlaceDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual IList<PlaceItem> Place { get; set; }
    }
}
