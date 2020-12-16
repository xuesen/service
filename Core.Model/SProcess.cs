using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public partial class SProcess : DomainObject, IAggregateRoot
    {
        public virtual SProcessType SProcessType { get; set; }

        public virtual SStage SStage { get; set; }

        public virtual string Name { get; set; }

        public virtual string Code { get; set; }

        public virtual string Description { get; set; }

        public virtual string Description2 { get; set; }
    }
}
