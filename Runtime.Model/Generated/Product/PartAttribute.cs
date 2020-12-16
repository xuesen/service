using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.Product
{
    public partial class PartAttribute : DomainObject
    {
        public virtual int PartId { get; set; }

        public virtual int AttributeId { get; set; }

        public virtual string Value { get; set; }
    }
}
