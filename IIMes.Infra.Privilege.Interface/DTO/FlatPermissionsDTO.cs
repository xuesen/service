using System;
using System.Collections.Generic;
using IIMes.Infra.Privilege.Model;
using ProtoBuf;

namespace IIMes.Infra.Privilege.Interface
{
    [ProtoContract]
    public class FlatPermissionsDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual ResourceDTO resources { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual IList<PermissionDTO> Permissions { get; set; }
    }
}