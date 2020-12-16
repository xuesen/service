using System;
using System.Collections.Generic;
using IIMes.Services.Runtime.Model;
using IIMes.Services.Runtime.Model.Plant;
using IIMes.Services.Runtime.Model.Process;
using IIMes.Services.Runtime.Model.Resource;
using ProtoBuf;

namespace IIMes.Services.Runtime.Interface.DTO
{
    public class TerminalDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int? Id { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual string Code { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual string Name { get; set; }

        [ProtoMember(6, DataFormat = DataFormat.WellKnown)]
        public virtual  LayerPdLineDTO Pdline { get; set; }

        [ProtoMember(7, DataFormat = DataFormat.WellKnown)]
        public virtual LayerProcessDTO Process { get; set; }

        [ProtoMember(8, DataFormat = DataFormat.WellKnown)]
        public virtual LayerEquipmentDTO Equipment { get; set; }

    }
}