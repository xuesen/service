using System;
using IIMes.Infrastructure.DomainObject;
using IIMes.Services.Runtime.Model.Resource;

namespace IIMes.Services.Runtime.Model.Plant
{
    public partial class Terminal : DomainObject, IAggregateRoot
    {
        public virtual ProductLine ProductLine { get; set; }

        public virtual Process.Process Process { get; set; }

        public virtual Equipment Equipment { get; set; }

        public virtual string Code { get; set; }

        public virtual string Name { get; set; }

        public virtual string Ip { get; set; }

        public virtual string Description { get; set; }

        public virtual string Description2 { get; set; }
    }
}
