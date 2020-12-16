using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.System
{
    public partial class ProcessFunction : DomainObject, IAggregateRoot
    {
        public virtual int ProcessId { get; set; }

        public virtual int FunctionId { get; set; }
    }
}
