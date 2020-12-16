using IIMes.Infrastructure.DomainObject;
using IIMes.Services.Runtime.Model.Process;
using IIMes.Services.Runtime.Model.System;

namespace IIMes.Services.Runtime.Model.Product
{
    public partial class Part : DomainObject, IAggregateRoot
    {
        public virtual Workflow Workflow { get; set; }

        public virtual Vendor Vendor { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Family Family { get; set; }

        public virtual Setting Setting { get; set; }

        public virtual string PartNo { get; set; }

        public virtual string PartName { get; set; }

        public virtual string Type { get; set; }

        public virtual string Spec1 { get; set; }

        public virtual string Spec2 { get; set; }

        public virtual string Version { get; set; }

        public virtual string PartDl { get; set; }

        public virtual string PartXl { get; set; }

        public virtual string VendorPartNo { get; set; }

        public virtual string CustomerPartNo { get; set; }

        public virtual string Uom { get; set; }

        public virtual bool? Keypart { get; set; }

        public virtual string MatchRule { get; set; }

        public virtual bool? UniqueCheck { get; set; }
    }
}
