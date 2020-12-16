using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.WOR
{
    public partial class WorScrap : DomainObject, IAggregateRoot
    {
        public virtual string WorkOrder { get; set; }

        public virtual int Qty { get; set; }

        public virtual string ScrapCode { get; set; }
    }
}
