using System;
using System.Collections.Generic;
using IIMes.Services.Runtime.Model;
using ProtoBuf;

namespace IIMes.Services.Runtime.Interface.DTO
{
    public class FeedingRequestDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int TerminalId { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual int? EquipmentId { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual string WorkOrder { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual int Status { get; set; }

        [ProtoMember(5, DataFormat = DataFormat.WellKnown)]
        public virtual string Editor { get; set; }

        [ProtoMember(6, DataFormat = DataFormat.WellKnown)]
        public virtual int? ItemId { get; set; }
    }
}