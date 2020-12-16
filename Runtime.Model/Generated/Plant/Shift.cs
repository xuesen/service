using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.Plant
{
    public class Shift : DomainObject, IAggregateRoot
    {
        public virtual ShiftGroup ShiftGroup { get; set; }

        public virtual string Code { get; set; }

        public virtual string Name { get; set; }

        public virtual string StartTime { get; set; }

        public virtual string EndTime { get; set; }
    }
}
