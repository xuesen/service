using System;
using System.Collections.Generic;
using IIMes.Infra.Privilege.Model;
using ProtoBuf;

namespace IIMes.Infra.Privilege.Interface
{
    [ProtoContract]
    public class LoginDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual EmployeeDTO User { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual IList<FlatPermissionsDTO> FlatPermissions { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual IList<PermissionsDTO> Permissions { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual string Token { get; set; }
    }
}