using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Infra.Privilege.Model
{
    
    public partial class RoleDepartment : DomainObject , IAggregateRoot
    {
        public virtual Role SRole { get; set; }
        public virtual Department SDepartment { get; set; }
        public virtual string UpdateEditor { get; set; }
    }
}
