using System;
using System.Text;
using System.Collections.Generic;
using IIMes.Infrastructure.DomainObject;

namespace Consumer.TestLog.DomainObject
{
    public class TestLog : IIMes.Infrastructure.DomainObject.DomainObject, IAggregateRoot
    {
        public virtual string Type { get; set; }
        public virtual string Model { get; set; }
        public virtual string BigPcbBarcode { get; set; }
        public virtual string Side { get; set; }
        public virtual string Result { get; set; }
        public virtual string EquipmentCode { get; set; }
        public virtual string ShiftCode { get; set; }
        public virtual string ProductLineCode { get; set; }
        public virtual string PcName { get; set; }
        public virtual DateTime TestTime { get; set; }
    }
}
