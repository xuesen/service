using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.System
{
    public partial class Attribute : DomainObject, IAggregateRoot
    {
        public virtual string AttributeName { get; set; }

        public virtual string AttributeCode { get; set; }

        public virtual string Type { get; set; }

        public virtual string Elements { get; set; }

        public virtual int Sequence { get; set; }

        public virtual string MustInput { get; set; }

        public virtual string RuleSet { get; set; }

        public virtual string Description { get; set; }

        public virtual string Editable { get; set; }
    }
}
