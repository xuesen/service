using System;
using ProtoBuf;

namespace IIMes.Services.Runtime.Interface
{
    [ProtoContract]
    public class LayerFactoryDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int Id { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual string Name { get; set; }
    }
}
