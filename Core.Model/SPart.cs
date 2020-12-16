using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public class SPart : DomainObject, IAggregateRoot
    {
        public virtual SWorkflow SWorkflow { get; set; }

        public virtual SVendor SVendor { get; set; }

        public virtual SCustomer SCustomer { get; set; }

        public virtual SFamily SFamily { get; set; }

        public virtual SSetting SSetting { get; set; }

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

        public virtual bool Keypart { get; set; }

        public virtual string MatchRule { get; set; }

        public virtual bool UniqueCheck { get; set; }
    }
}
