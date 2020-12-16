using System;
using System.Collections.Generic;
using IIMes.Services.Core.Model;
using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class WorkFlowDisplayDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int Id { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual string Name { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual List<SWorkflowDTO> Workflows { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual SPartDTO Part { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual FamilyDTO Family { get; set; }
    }
}