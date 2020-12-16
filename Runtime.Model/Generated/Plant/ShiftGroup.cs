using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.Plant
{
    public class ShiftGroup : DomainObject, IAggregateRoot
    {
        public virtual string Code { get; set; }

        public virtual string Name { get; set; }
    }
}
