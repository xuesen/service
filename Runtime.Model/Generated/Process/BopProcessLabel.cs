using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.Process
{
    public partial class BopProcessLabel : DomainObject, IAggregateRoot
    {
        public virtual int BopBaseID { get; set; }

        public virtual int ProcessId { get; set; }

        public virtual string SpecialCheck { get; set; }

        public virtual int PrintTemplateId { get; set; }

        public virtual int Piece { get; set; }

        public virtual int Trigger { get; set; }

        public virtual int Priority { get; set; }

        public virtual bool Enabled { get; set; }
    }
}
