using System;
using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class SNODTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual string Sno { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual string Wc { get; set; }
    }
}