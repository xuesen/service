using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public partial class SBomLocation : DomainObject, IAggregateRoot
    {
        public virtual int BomItemId { get; set; }

        public virtual string Location { get; set; }
    }
}
