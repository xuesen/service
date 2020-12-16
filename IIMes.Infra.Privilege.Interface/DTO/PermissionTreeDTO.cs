using System;
using System.Collections.Generic;
using ProtoBuf;

namespace IIMes.Infra.Privilege.Interface
{
    [ProtoContract]
    public class PermissionTreeDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual ResourceDTO Resources { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual IList<PermissionTreeDTO> Children { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual IList<ResourcePermissionsDTO>  ResourcePermissions { get; set; }
    }
}