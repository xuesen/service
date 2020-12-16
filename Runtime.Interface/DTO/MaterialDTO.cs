using System;
using System.Collections.Generic;
using IIMes.Services.Runtime.Model;
using ProtoBuf;

namespace IIMes.Services.Runtime.Interface.DTO
{
    public class MaterialDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual string ReelId { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual string Pn { get; set; }
    }
}