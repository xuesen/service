using System;
using System.Collections.Generic;
using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class WoBomPartDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual CommonDTO ItemPart { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual IList<CommonDTO> SubPart { get; set; }
    }
}
