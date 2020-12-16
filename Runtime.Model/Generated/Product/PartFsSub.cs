using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.Product
{
    public partial class PartFsSub : DomainObject
    {
        public virtual int PartFsDetailId { get; set; }

        public virtual int SubPartId { get; set; }
    }
}
