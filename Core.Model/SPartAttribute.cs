using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public class SPartAttribute : DomainObject, IAggregateRoot
    {
        public virtual SPart SPart { get; set; }

        public virtual SAttribute SAttribute { get; set; }

        public virtual string Value { get; set; }
    }
}
