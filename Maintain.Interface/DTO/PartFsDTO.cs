using System;
using System.Collections.Generic;
using IIMes.Services.Core.Model;
using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class PartFsDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int PdLineId { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual int EquipmentId { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual int PartId { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual string Side { get; set; }

        [ProtoMember(5, DataFormat = DataFormat.WellKnown)]
        public virtual string XmlFile { get; set; }

        [ProtoMember(6, DataFormat = DataFormat.WellKnown)]
        public virtual string EmployeeCode { get; set; }
    }
}
