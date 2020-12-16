using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Infra.Privilege.Model
{
    
    public partial class RoleEmployee : DomainObject , IAggregateRoot
    {
        public virtual Role SRole { get; set; }
        public virtual Employee SEmployee { get; set; }
        public virtual string UpdateEditor { get; set; }
    }
}
