using System;
using System.Collections.Generic;
using IIMes.Services.Core.Model;
using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class CommonWoFsDetailDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int Id { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual string WorkOrder { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual string Station { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual int ItemPartId { get; set; }

        [ProtoMember(5, DataFormat = DataFormat.WellKnown)]
        public virtual string ItemPartNo { get; set; }

        [ProtoMember(6, DataFormat = DataFormat.WellKnown)]
        public virtual string ItemPartName { get; set; }

        [ProtoMember(7, DataFormat = DataFormat.WellKnown)]
        public virtual IList<CommonWoFsSubDTO> WoFsSub { get; set; }

        [ProtoMember(8, DataFormat = DataFormat.WellKnown)]
        public virtual int ItemCount { get; set; }

        [ProtoMember(9, DataFormat = DataFormat.WellKnown)]
        public virtual string Location { get; set; }

        [ProtoMember(10, DataFormat = DataFormat.WellKnown)]
        public virtual string FeederType { get; set; }

        [ProtoMember(11, DataFormat = DataFormat.WellKnown)]
        public virtual string FeederSpec { get; set; }
    }
}
