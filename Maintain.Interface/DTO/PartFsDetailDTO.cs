using System;
using System.Collections.Generic;
using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class PartFsDetailDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual string Station { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual string ItemPartNo { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual int Qty { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual IList<string> Location { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual string FeedingType { get; set; }

        [ProtoMember(5, DataFormat = DataFormat.WellKnown)]
        public virtual string FeedingSpec { get; set; }
    }
}
