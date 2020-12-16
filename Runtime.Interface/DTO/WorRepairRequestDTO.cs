using System;
using System.Collections.Generic;
using ProtoBuf;

namespace IIMes.Services.Runtime.Interface.DTO
{
    [ProtoContract]
    public class WorRepairRequestDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual string SN { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual string TerminalCode { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual string Editor { get; set; }
    }
}
