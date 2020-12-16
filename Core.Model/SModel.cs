using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public class SModel : DomainObject, IAggregateRoot
    {
        public virtual SCustomer SCustomer { get; set; }

        public virtual SFamily SFamily { get; set; }

        public virtual string PartNo { get; set; }

        public virtual string PartName { get; set; }
    }
}
