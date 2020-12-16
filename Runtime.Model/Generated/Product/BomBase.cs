using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.Product
{
    public partial class BomBase : DomainObject, IAggregateRoot
    {
        public virtual Part Part { get; set; }

        public virtual string Name { get; set; }

        public virtual string LastVersion { get; set; }
    }
}
