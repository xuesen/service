using System;
using ProtoBuf;

namespace IIMes.Infra.Privilege.Interface
{
    [ProtoContract]
    public class RoleEmployeeDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int? Id { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual int RoleId { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual int EmployeeId { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual string EmployeeCode { get; set; }

        [ProtoMember(5, DataFormat = DataFormat.WellKnown)]
        public virtual string EmployeeName { get; set; }

        [ProtoMember(6, DataFormat = DataFormat.WellKnown)]
        public virtual LayerEditorDTO Editor { get; set; }

        [ProtoMember(7, DataFormat = DataFormat.WellKnown)]
        public virtual DateTime Update_time { get; set; }

        [ProtoMember(8, DataFormat = DataFormat.WellKnown)]
        public virtual string UpdateEditor { get; set; }
    }
}