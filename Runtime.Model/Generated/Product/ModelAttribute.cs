using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.Product
{
    public partial class ModelAttribute : DomainObject
    {
        public virtual int ModelId { get; set; }

        public virtual int AttributeId { get; set; }

        public virtual string Value { get; set; }
    }
}
