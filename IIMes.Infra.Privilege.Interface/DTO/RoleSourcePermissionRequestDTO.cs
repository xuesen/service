using System;
using System.Collections.Generic;
using IIMes.Infra.Privilege.Model;
using ProtoBuf;

namespace IIMes.Infra.Privilege.Interface
{
    [ProtoContract]
    public class RoleSourcePermissionRequestDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int RoleId { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual IList<int> NodesChecked { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual IList<ResourcePermissionDTO> ResourcePermissions { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual string Editor { get; set; }
    }
}