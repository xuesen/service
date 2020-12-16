using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.Product
{
    public partial class BomItem : DomainObject, IAggregateRoot
    {
        public virtual Bom Bom { get; set; }

        public virtual Part Part { get; set; }

        public virtual decimal ItemCount { get; set; }

        public virtual string ItemGroup { get; set; }

        public virtual string ItemVersion { get; set; }

        public virtual string PrimaryKey { get; set; }
    }
}
