using System;
using System.Collections.Generic;
using IIMes.Infra.Privilege.Model;
using ProtoBuf;

namespace IIMes.Infra.Privilege.Interface
{
    [ProtoContract]
    public class ResourcePermissionsDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int ResourcePermissionId { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual PermissionDTO Permission { get; set; }
    }
}