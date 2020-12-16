using System;
using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class EmployeeDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int? Id { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual LayerEditorDTO Editor { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual string Code { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual string Password { get; set; }

        [ProtoMember(5, DataFormat = DataFormat.WellKnown)]
        public virtual string Name { get; set; }

        [ProtoMember(6, DataFormat = DataFormat.WellKnown)]
        public virtual string Duty { get; set; }

        [ProtoMember(7, DataFormat = DataFormat.WellKnown)]
        public virtual string Email { get; set; }

        [ProtoMember(8, DataFormat = DataFormat.WellKnown)]
        public virtual string PhoneNo { get; set; }

        [ProtoMember(9, DataFormat = DataFormat.WellKnown)]
        public virtual string Description { get; set; }

        [ProtoMember(10, DataFormat = DataFormat.WellKnown)]
        public virtual string Type { get; set; }

        [ProtoMember(11, DataFormat = DataFormat.WellKnown)]
        public virtual string Domain { get; set; }

        [ProtoMember(12, DataFormat = DataFormat.WellKnown)]
        public virtual DateTime? Leave_office_time { get; set; }

        [ProtoMember(13, DataFormat = DataFormat.WellKnown)]
        public virtual LayerDepartmentDTO SDepartment { get; set; }

        [ProtoMember(14, DataFormat = DataFormat.WellKnown)]
        public virtual LayerEmployeeDTO SEmployee { get; set; }

        [ProtoMember(15, DataFormat = DataFormat.WellKnown)]
        public virtual DateTime Update_time { get; set; }
    }
}
