using System;
using System.Collections.Generic;
using IIMes.Services.Runtime.Model;
using ProtoBuf;

namespace IIMes.Services.Runtime.Interface.DTO
{
    public class PartItemDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int Id { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual string PartNo { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual string PartName { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual string SPEC1 { get; set; }
    }
}