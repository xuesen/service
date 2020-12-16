using System;
using ProtoBuf;

namespace IIMes.Infra.Privilege.Interface
{
    [ProtoContract]
    public class EmployeeManagerRequestDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual string Manager { get; set; }
    }
}