using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.Production
{
    public class FsStatusLog : DomainObject, IAggregateRoot
    {
        public virtual int? TerminalId { get; set; }

        public virtual int? EquipmentId { get; set; }

        public virtual string WorkOrder { get; set; }

        // public virtual string Side { get; set; }

        public virtual int? Status { get; set; }
    }
}
