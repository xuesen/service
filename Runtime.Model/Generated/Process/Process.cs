using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.Process
{
    public partial class Process : DomainObject, IAggregateRoot
    {
        public virtual ProcessType ProcessType { get; set; }

        public virtual Stage Stage { get; set; }

        public virtual string Name { get; set; }

        public virtual string Code { get; set; }

        public virtual string Description { get; set; }

        public virtual string Description2 { get; set; }
    }
}
