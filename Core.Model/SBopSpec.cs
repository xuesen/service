using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public partial class SBopSpec : DomainObject, IAggregateRoot
    {
        public virtual SBopBase SBopBase { get; set; }

        public virtual string ParamKey { get; set; }

        public virtual SBarcodeRule ParamValue { get; set; }
    }
}
