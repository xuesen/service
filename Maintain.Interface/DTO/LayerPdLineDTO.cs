using System;
using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class LayerPdLineDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int Id { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual string Name { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual LayerWorkShopDTO Workshop { get; set; }
    }
}
