using System;
using System.Collections.Generic;
using IIMes.Services.Core.Model;
using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class BomListDetailDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int Id { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual string Version { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual string Status { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual string Editor { get; set; }

        [ProtoMember(5, DataFormat = DataFormat.WellKnown)]
        public virtual List<LayerBomItemsDTO> BomItems { get; set; }
    }
}