using System;
using System.Collections.Generic;
using IIMes.Infra.Privilege.Model;
using ProtoBuf;

namespace IIMes.Infra.Privilege.Interface
{
    [ProtoContract]
    public class ResourcePermissionDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int ResourceId { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual IList<int> ResourcePermissionIds { get; set; }
    }
}