using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.System
{
    public partial class BarcodeRule : DomainObject, IAggregateRoot
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
