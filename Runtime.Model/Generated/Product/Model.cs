using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.Product
{
    public partial class Model : DomainObject, IAggregateRoot
    {
        public virtual string PartNo { get; set; }

        public virtual string PartName { get; set; }

        public virtual int CustomerId { get; set; }

        public virtual int FamilyId { get; set; }
    }
}
