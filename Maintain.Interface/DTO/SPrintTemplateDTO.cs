using System;
using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class SPrintTemplateDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int Id { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual string Name { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual string FileString { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual string Description { get; set; }

        [ProtoMember(5, DataFormat = DataFormat.WellKnown)]
        public virtual string SpName { get; set; }

        [ProtoMember(6, DataFormat = DataFormat.WellKnown)]
        public virtual int Rotate180 { get; set; }

        [ProtoMember(7, DataFormat = DataFormat.WellKnown)]
        public virtual int Layout { get; set; }

        [ProtoMember(8, DataFormat = DataFormat.WellKnown)]
        public virtual bool Collate { get; set; }

        [ProtoMember(9, DataFormat = DataFormat.WellKnown)]
        public virtual int PrintMode { get; set; }

        [ProtoMember(10, DataFormat = DataFormat.WellKnown)]
        public virtual int Continuous { get; set; }

        [ProtoMember(11, DataFormat = DataFormat.WellKnown)]
        public virtual int PrintKey { get; set; }

        [ProtoMember(12, DataFormat = DataFormat.WellKnown)]
        public virtual string Remark { get; set; }

        [ProtoMember(13, DataFormat = DataFormat.WellKnown)]
        public virtual string Editor { get; set; }

        [ProtoMember(14, DataFormat = DataFormat.WellKnown)]
        public virtual string PrintKeyName { get; set; }
    }
}