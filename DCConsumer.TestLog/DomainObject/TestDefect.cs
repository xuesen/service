using System;
using System.Text;
using System.Collections.Generic;
using IIMes.Infrastructure.DomainObject;

namespace Consumer.TestLog.DomainObject
{
    public class TestDefect : IIMes.Infrastructure.DomainObject.DomainObject, IAggregateRoot
    {
        public virtual int LogId { get; set; }
        public virtual string Type { get; set; }
        public virtual string PartNo { get; set; }
        public virtual string ComponentCode { get; set; }
        public virtual string ComponentName { get; set; }
        public virtual string ComponentLocation { get; set; }
        public virtual string Defect { get; set; }
        public virtual DateTime TestTime { get; set; }
    }
}
