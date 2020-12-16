using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Infra.Privilege.Model
{
    public partial class Department : DomainObject, IAggregateRoot
    {
        public virtual Department SDepartment { get; set; }        
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string Company { get; set; }
        public virtual string UpdateEditor { get; set; }
    }
}
