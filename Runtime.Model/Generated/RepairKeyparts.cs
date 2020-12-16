using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model
{
    public partial class RepairKeyparts : DomainObject, IAggregateRoot
    {
        public virtual string SerialNumber { get; set; }

        public virtual int? RepairLogId { get; set; }

        public virtual int OldPartId { get; set; }

        public virtual string OldPartNo { get; set; }

        public virtual string OldPartSn { get; set; }

        public virtual int NewPartId { get; set; }

        public virtual string NewPartNo { get; set; }

        public virtual string NewPartSn { get; set; }

        public virtual string Remark { get; set; }
    }
}
