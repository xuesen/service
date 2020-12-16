using System;
using System.Collections.Generic;
using IIMes.Services.Runtime.Model;
using IIMes.Services.Runtime.Model.Process;
using ProtoBuf;

namespace IIMes.Services.Runtime.Interface.DTO
{
    public class WoRequestDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual IList<string> WoStatus { get; set; }
    }
}