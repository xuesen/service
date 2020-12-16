using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public partial class SBomItem : DomainObject, IAggregateRoot
    {
        public virtual SBom SBom { get; set; }

        public virtual SPart SPart { get; set; }

        public virtual decimal ItemCount { get; set; }

        public virtual string ItemGroup { get; set; }

        public virtual string ItemVersion { get; set; }

        public virtual string PrimaryKey { get; set; }
    }
}
