using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.Product
{
    public partial class Bom : DomainObject, IAggregateRoot
    {
        public virtual BomBase BomBase { get; set; }

        public virtual string Version { get; set; }

        public virtual string Status { get; set; }

        public virtual string Description { get; set; }
    }
}
