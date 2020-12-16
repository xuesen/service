using System;
using System.Collections.Generic;
using IIMes.Services.Core.Model;
using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class EqualWoFsDetailDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        // public virtual WoFsDetail FsDetail { get; set; }
        public virtual string Station { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual int ItemPartId { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual int ItemCount { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual string Location { get; set; }

        [ProtoMember(5, DataFormat = DataFormat.WellKnown)]
        public virtual string FeederType { get; set; }

        [ProtoMember(6, DataFormat = DataFormat.WellKnown)]
        public virtual string FeederSpec { get; set; }

        [ProtoMember(7, DataFormat = DataFormat.WellKnown)]
        public virtual IList<int> FsSubLst { get; set; }
    }
}
