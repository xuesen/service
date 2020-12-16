using System;
using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class OptionsRequestDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual string Options { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual string Value { get; set; }
    }
}