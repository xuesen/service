using System;
using System.Collections.Generic;
using ProtoBuf;

namespace IIMes.Infra.Privilege.Interface
{
    [ProtoContract]
    public class PermissionTreeByRoleDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual ResourceDTO Resource { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual IList<ResourcePermissionsDTO>  ResourcePermissions { get; set; }
    }
}