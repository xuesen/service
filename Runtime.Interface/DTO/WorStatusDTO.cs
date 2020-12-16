using System;
using ProtoBuf;

namespace IIMes.Services.Runtime.Interface.DTO
{
    [ProtoContract]
    public class WorStatusDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual string WorkOrder { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual int PassQty { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual int FailQty { get; set; }


    }
}
