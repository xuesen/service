using System.Collections.Generic;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.Process
{
    public partial class BopBase : DomainObject, IAggregateRoot
    {
        public virtual int? PartId { get; set; }

        public virtual int FamilyId { get; set; }

        public virtual int WorkflowId { get; set; }

        public virtual string Status { get; set; }
    }
}
