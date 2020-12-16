using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public class SShift : DomainObject, IAggregateRoot
    {
        public virtual SShiftGroup SShiftGroup { get; set; }

        public virtual string Code { get; set; }

        public virtual string Name { get; set; }

        public virtual string StartTime { get; set; }

        public virtual string EndTime { get; set; }
    }
}
