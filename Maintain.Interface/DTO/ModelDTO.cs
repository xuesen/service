using System;
using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class ModelDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int? Id { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual string Code { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual string Name { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual string Type { get; set; }

        [ProtoMember(5, DataFormat = DataFormat.WellKnown)]
        public virtual string Rule { get; set; }

        [ProtoMember(6, DataFormat = DataFormat.WellKnown)]
        public virtual string Description { get; set; }

        [ProtoMember(7, DataFormat = DataFormat.WellKnown)]
        public virtual LayerEditorDTO Editor { get; set; }
    }
}
