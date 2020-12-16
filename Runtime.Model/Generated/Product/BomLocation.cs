using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.Product
{
    public partial class BomLocation : DomainObject, IAggregateRoot
    {
        public virtual int BomItemId { get; set; }

        public virtual string Location { get; set; }
    }
}
