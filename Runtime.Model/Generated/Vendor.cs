using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model
{
    public partial class Vendor : DomainObject
    {
        public virtual string Code { get; set; }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual string Description2 { get; set; }
    }
}
