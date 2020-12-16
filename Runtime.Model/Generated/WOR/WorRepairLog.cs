using System;
using IIMes.Infrastructure.DomainObject;


namespace IIMes.Services.Runtime.Model.WOR {
    
    public partial class WorRepairLog : DomainObject, IAggregateRoot
    {
        public virtual string WorkOrder { get; set; }

        public virtual string SerialNumber { get; set; }

        public virtual int TestWorLogId { get; set; }

        public virtual int ProcessId { get; set; }

        public virtual string ProcessCode { get; set; }

        public virtual string Repairer { get; set; }

        public virtual int? RepairWorLogId { get; set; }

        public virtual int? RepairTime { get; set; }

        public virtual DateTime? UpdateTime { get; set; }
        
    }
}
