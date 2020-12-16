using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public partial class SBopProcessBom : DomainObject, IAggregateRoot
    {
        public virtual SBopBase BopBase { get; set; }

        public virtual SProcess Process { get; set; }

        public virtual SPart ItemPart { get; set; }

        public virtual int? Qty { get; set; }

        public virtual string Location { get; set; }
    }
}
