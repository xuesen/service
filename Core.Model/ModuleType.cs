using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public partial class ModuleType : DomainObject, IAggregateRoot
    {
        public virtual string Type { get; set; }

        public virtual string Conditions { get; set; }

        public virtual int ConditionComposition { get; set; }
    }
}
