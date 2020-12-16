using System;
using ProtoBuf;

namespace IIMes.Infra.Privilege.Interface
{
    public class DepartmentDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int? Id { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual string Code { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual string Name { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual string Company { get; set; }

        [ProtoMember(5, DataFormat = DataFormat.WellKnown)]
        public virtual string Description { get; set; }

        [ProtoMember(6, DataFormat = DataFormat.WellKnown)]
        public virtual LayerEditorDTO Editor { get; set; }

        [ProtoMember(7, DataFormat = DataFormat.WellKnown)]
        public virtual LayerDTO SDepartment { get; set; }

        [ProtoMember(8, DataFormat = DataFormat.WellKnown)]
        public virtual DateTime Update_time { get; set; }

        [ProtoMember(9, DataFormat = DataFormat.WellKnown)]
        public virtual string UpdateEditor { get; set; }
    }
}
