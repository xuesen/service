using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public class SDepartment : DomainObject, IAggregateRoot
    {
        public virtual string Code { get; set; }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual string Company { get; set; }

        public virtual SDepartment Department { get; set; }

        // public virtual string DepartmentId { get; set; }
    }
}
