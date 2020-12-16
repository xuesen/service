using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public partial class RepairLog : DomainObject, IAggregateRoot
    {
        public virtual string SerialNumber { get; set; }

        public virtual int TestProductLogId { get; set; }

        public virtual int TesterId { get; set; }

        public virtual string Tester { get; set; }

        public virtual int ProcessId { get; set; }

        public virtual string ProcessCode { get; set; }

        public virtual int RepairerId { get; set; }

        public virtual string Repairer { get; set; }

        public virtual int? RepairProductLogId { get; set; }

        public virtual int? RepairTime { get; set; }

        public virtual DateTime CreateTime { get; set; }

        public virtual DateTime? UpdateTime { get; set; }
    }
}
