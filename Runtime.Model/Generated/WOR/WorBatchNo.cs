using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.WOR
{
    public partial class WorBatchNo : DomainObject, IAggregateRoot
    {
        public virtual string WorkOrder { get; set; }

        public virtual string BatchNo { get; set; }

        public virtual int PartId { get; set; }

        public virtual string PartNo { get; set; }

        public virtual int ProcessId { get; set; }

        public virtual string ProcessCode { get; set; }

        public virtual int Qty { get; set; }
    }
}
