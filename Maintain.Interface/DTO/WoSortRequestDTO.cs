using System;
using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class WoSortRequestDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int FromId { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual int ToId { get; set; }
    }
}
