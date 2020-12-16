using System;
using System.Collections.Generic;
using IIMes.Services.Runtime.Model;
using ProtoBuf;

namespace IIMes.Services.Runtime.Interface.DTO
{
    public class WorScrapRequestDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual string WorkOrder { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual int Qty { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual string ScrapReason { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual string Editor { get; set; }
    }
}