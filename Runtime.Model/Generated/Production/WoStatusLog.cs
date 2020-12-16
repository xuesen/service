using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.Production
{
    public partial class WoStatusLog : DomainObject, IAggregateRoot
    {
        public virtual WoBase WoBase { get; set; }

        public virtual WoStatus WoStatus { get; set; }

        public virtual string Reason { get; set; }
    }
}
