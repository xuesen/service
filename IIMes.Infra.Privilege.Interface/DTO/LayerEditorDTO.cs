using System;
using ProtoBuf;

namespace IIMes.Infra.Privilege.Interface
{
    public class LayerEditorDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual string Name { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual string Code { get; set; }
    }
}
