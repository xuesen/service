using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.Production
{
    public partial class FsStatus : DomainObject, IAggregateRoot
    {
        public virtual int? TerminalId { get; set; }

        public virtual int? EquipmentId { get; set; }

        public virtual string WorkOrder { get; set; }

        public virtual int? Status { get; set; }
    }
}
