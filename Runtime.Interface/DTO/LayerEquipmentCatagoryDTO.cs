using System;
using ProtoBuf;

namespace IIMes.Services.Runtime.Interface
{
    [ProtoContract]
    public class LayerEquipmentCatagoryDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int Id { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual string Name { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual LayerSettingDTO Setting { get; set; }
    }
}
