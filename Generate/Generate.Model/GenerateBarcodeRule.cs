using System;
using IIMes.Infrastructure.DomainObject;

namespace Generate.Model
{
    public partial class GenerateBarcodeRule : DomainObject, IAggregateRoot
    {
        public virtual string RuleName { get; set; }

        public virtual int RuleType { get; set; }

        public virtual string RuleDesc { get; set; }

        public virtual string SerialJson { get; set; }

        public virtual string Prefix { get; set; }

        public virtual string Suffix { get; set; }

        public virtual string Status { get; set; }
    }
}
