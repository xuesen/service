using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public partial class SBopProcessLabel : DomainObject, IAggregateRoot
    {
        public virtual int BopBaseId { get; set; }

        public virtual int ProcessId { get; set; }

        public virtual string SpecialCheck { get; set; }

        public virtual SPrintTemplate PrintTemplate { get; set; }

        public virtual int Piece { get; set; }

        public virtual SSetting SSetting { get; set; }

        public virtual int Priority { get; set; }
    }
}
