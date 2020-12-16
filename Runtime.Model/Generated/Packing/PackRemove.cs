using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.Packing
{
    public partial class PackRemove : DomainObject, IAggregateRoot
    {
        public virtual string Type { get; set; }

        public virtual string RemoveNo { get; set; }

        public virtual string Remark { get; set; }
    }
}
