using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.Process
{
    public partial class BopProcessBom : DomainObject, IAggregateRoot
    {
        public virtual int BopBaseId { get; set; }

        public virtual int ProcessId { get; set; }

        public virtual int ItemPartId { get; set; }

        public virtual int Qty { get; set; }

        public virtual string Location { get; set; }
    }
}
