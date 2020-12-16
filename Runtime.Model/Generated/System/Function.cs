using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.System
{
    public partial class Function : DomainObject, IAggregateRoot
    {
        public virtual string Code { get; set; }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }
    }
}
