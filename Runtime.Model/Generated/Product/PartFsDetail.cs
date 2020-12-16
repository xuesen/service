using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.Product
{
    public partial class PartFsDetail : DomainObject
    {
        public virtual int PartFsId { get; set; }

        public virtual string Station { get; set; }

        public virtual int ItemPartId { get; set; }

        public virtual int ItemCount { get; set; }

        public virtual string Location { get; set; }

        public virtual string FeederType { get; set; }

        public virtual string FeederSpec { get; set; }
    }
}
