using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public partial class SBom : DomainObject, IAggregateRoot
    {
        public virtual SBomBase SBomBase { get; set; }

        public virtual string Version { get; set; }

        public virtual string Status { get; set; }

        public virtual string Description { get; set; }
    }
}
