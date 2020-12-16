using System;
using IIMes.Services.Core.Model;
using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class SBopSpecDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int? Id { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual int BopBaseId { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual string ParamKey { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual SBarcodeRule ParamValue { get; set; }

        [ProtoMember(5, DataFormat = DataFormat.WellKnown)]
        public virtual string Editor { get; set; }
    }
}