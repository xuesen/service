using System;
using ProtoBuf;

namespace IIMes.Services.Maintain.Interface
{
    [ProtoContract]
    public class BomItemDataDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int ItemPartId { get; set; }
    }
}
