using System;
using IIMes.Services.Core.Model;
using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class ProgramDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual ProgramItem Program { get; set; }
    }
}
