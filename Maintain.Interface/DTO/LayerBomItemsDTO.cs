using System;
using System.Collections.Generic;
using IIMes.Services.Core.Model;
using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class LayerBomItemsDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int Id { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual decimal ItemCount { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual string ItemGroup { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual string ItemVersion { get; set; }

        [ProtoMember(5, DataFormat = DataFormat.WellKnown)]
        public virtual string PrimaryKey { get; set; }

        [ProtoMember(6, DataFormat = DataFormat.WellKnown)]
        public virtual SPartDTO Part { get; set; }

        [ProtoMember(7, DataFormat = DataFormat.WellKnown)]
        public virtual List<SBomLocation> Locations { get; set; }
    }
}
