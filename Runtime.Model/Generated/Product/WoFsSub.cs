using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.Product
{
    public partial class WoFsSub : DomainObject
    {
        public virtual int WoFsDetailId { get; set; }

        public virtual int SubPartId { get; set; }
    }
}
