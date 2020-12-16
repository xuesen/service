using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public class SAttribute : DomainObject, IAggregateRoot
    {
        public virtual string AttributeName { get; set; }

        public virtual string AttributeCode { get; set; }

        public virtual string Type { get; set; }

        public virtual string Elements { get; set; }

        public virtual int Sequence { get; set; }

        public virtual string Mustinput { get; set; }

        public virtual string Ruleset { get; set; }

        public virtual string Description { get; set; }

        public virtual int Editable { get; set; }

        public virtual DateTime CreateTime { get; set; }

        public virtual DateTime UpdateTime { get; set; }
    }
}
