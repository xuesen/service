using System;
using ProtoBuf;

namespace IIMes.Infra.Privilege.Interface
{
    [ProtoContract]
    public class RoleResourcePermissionDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int? Id { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual int ResourcePermissionId { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual int RoleId { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual string Editor { get; set; }

        [ProtoMember(5, DataFormat = DataFormat.WellKnown)]
        public virtual DateTime Update_time { get; set; }

        [ProtoMember(6, DataFormat = DataFormat.WellKnown)]
        public virtual string UpdateEditor { get; set; }
    }
}