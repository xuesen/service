using System;
using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class ExcelBomDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual string PartNo { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual string Version { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual string ItemPartNo { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual string ItemVersion { get; set; }

        [ProtoMember(5, DataFormat = DataFormat.WellKnown)]
        public virtual string ItemCount { get; set; }

        [ProtoMember(6, DataFormat = DataFormat.WellKnown)]
        public virtual string ItemGroup { get; set; }

        [ProtoMember(7, DataFormat = DataFormat.WellKnown)]
        public virtual string PrimaryKey { get; set; }

        [ProtoMember(8, DataFormat = DataFormat.WellKnown)]
        public virtual string Location { get; set; }
    }
}