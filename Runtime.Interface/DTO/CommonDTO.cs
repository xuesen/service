﻿using System;
using System.Collections.Generic;
using IIMes.Services.Runtime.Model;
using ProtoBuf;

namespace IIMes.Services.Runtime.Interface.DTO
{
    public class CommonDTO
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual int Id { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual string Code { get; set; }

        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public virtual string Name { get; set; }

        [ProtoMember(4, DataFormat = DataFormat.WellKnown)]
        public virtual string Remark { get; set; }
    }
}