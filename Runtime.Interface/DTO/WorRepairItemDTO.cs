using System;
using System.Collections.Generic;
using IIMes.Services.Runtime.Model;
using ProtoBuf;

namespace IIMes.Services.Runtime.Interface.DTO
{
    public class WorRepairItemDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual IList<WorRepairItemInfoDTO> RepairItemInfo { get; set; }

        // repairlog表自增id
        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual int RepairLogId { get; set; }
    }
}