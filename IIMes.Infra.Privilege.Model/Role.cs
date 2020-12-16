using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Infra.Privilege.Model
{
    
    public partial class Role : DomainObject , IAggregateRoot
    {
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual string Type { get; set; }
        public virtual string Description { get; set; }
        public virtual string Application { get; set; }
        public virtual string UpdateEditor { get; set; }
    }
}
