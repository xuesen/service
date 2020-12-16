using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public partial class SFactory : DomainObject, IAggregateRoot
    {
        public virtual string Code { get; set; }

        public virtual string Name { get; set; }

        public virtual string Group { get; set; }

        public virtual string Description { get; set; }

        public virtual string Description2 { get; set; }
    }
}
