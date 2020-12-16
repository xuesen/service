using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Infra.Privilege.Model
{
    
    public partial class ResourcePermission : DomainObject , IAggregateRoot
    {
        public virtual Resource SResource { get; set; }
        public virtual Permission SPermission { get; set; }
        public virtual string UpdateEditor { get; set; }
    }
}
