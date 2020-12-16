using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Infra.Privilege.Model
{
    
    public partial class RoleResourcePermission : DomainObject , IAggregateRoot
    {
        public virtual ResourcePermission SResourcePermission { get; set; }
        public virtual Role SRole { get; set; }
        public virtual string UpdateEditor { get; set; }
    }
}
