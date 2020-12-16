using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Infra.Privilege.Model
{
    
    public partial class Resource : DomainObject , IAggregateRoot
    {
        public virtual Resource SResource { get; set; }
        public virtual string Application { get; set; }
        public virtual string SubApplication { get; set; }
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual bool? IsLeaf { get; set; }
        public virtual string Url { get; set; }
        public virtual string Icon { get; set; }
        public virtual int? Sequence { get; set; }
        public virtual string UpdateEditor { get; set; }
    }
}
