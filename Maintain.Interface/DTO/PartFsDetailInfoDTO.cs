using System;
using IIMes.Services.Core.Model;
using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class PartFsDetailInfoDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual SPartFsDetail PartFsDetail { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual string ItemPartNo { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual LayerEditorDTO Editor { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual DateTime Update_time { get; set; }
    }
}
