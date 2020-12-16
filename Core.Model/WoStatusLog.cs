using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public partial class WoStatusLog : DomainObject, IAggregateRoot
    {
        public virtual int WoBaseId { get; set; }

        public virtual WoBase WoBase { get; set; }

        public virtual int StatusId { get; set; }

        public virtual SWoStatus SWoStatus { get; set; }

        public virtual string Reason { get; set; }
    }
}
