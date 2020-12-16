using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public partial class STerminal : DomainObject, IAggregateRoot
    {
        public virtual SProductLine SProductLine { get; set; }

        public virtual SProcess SProcess { get; set; }

        public virtual SEquipment SEquipment { get; set; }

        public virtual string Code { get; set; }

        public virtual string Name { get; set; }

        public virtual string Ip { get; set; }

        public virtual string Description { get; set; }

        public virtual string Description2 { get; set; }
    }
}
