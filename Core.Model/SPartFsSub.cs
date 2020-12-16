using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public partial class SPartFsSub : DomainObject
    {
        public virtual int PartFsDetailId { get; set; }

        public virtual int SubPartId { get; set; }
    }
}
