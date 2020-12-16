using System;
using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class WoBomDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int? Id { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual LayerWoBaseDTO WoBase { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual LayerMainPartDTO MainPart { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual LayerSubPartDTO SubPart { get; set; }

        [ProtoMember(5, DataFormat = DataFormat.WellKnown)]
        public virtual string SubPartGroup { get; set; }

        [ProtoMember(6, DataFormat = DataFormat.WellKnown)]
        public virtual string SubPartQTY { get; set; }

        [ProtoMember(7, DataFormat = DataFormat.WellKnown)]
        public virtual LayerProcessDTO Process { get; set; }

        [ProtoMember(8, DataFormat = DataFormat.WellKnown)]
        public virtual string Version { get; set; }

        [ProtoMember(9, DataFormat = DataFormat.WellKnown)]
        public virtual string Location { get; set; }

        [ProtoMember(10, DataFormat = DataFormat.WellKnown)]
        public virtual LayerEditorDTO Editor { get; set; }

        [ProtoMember(11, DataFormat = DataFormat.WellKnown)]
        public virtual DateTime Update_time { get; set; }
    }
}
