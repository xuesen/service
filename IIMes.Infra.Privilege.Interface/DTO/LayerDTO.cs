using System;
using ProtoBuf;

namespace IIMes.Infra.Privilege.Interface
{
    [ProtoContract]
    public class LayerDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int? Id { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual string Name { get; set; }
    }
}
