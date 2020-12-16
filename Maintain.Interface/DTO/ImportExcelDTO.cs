using System;
using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class ImportExcelDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual object Files { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual string Entityname { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual string Editor { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual DateTime Time { get; set; }
    }
}
