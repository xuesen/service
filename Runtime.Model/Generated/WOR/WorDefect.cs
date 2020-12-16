using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.WOR {
    
    public partial class WorDefect : DomainObject, IAggregateRoot
    {
        public virtual string WorkOrder { get; set; }

        public virtual string SerialNumber { get; set; }

        public virtual int TestWorLogId { get; set; }

        public virtual int DefectId { get; set; }

        public virtual string DefectCode { get; set; }

        public virtual int ProcessId { get; set; }

        public virtual string ProcessCode { get; set; }

        public virtual string Remark { get; set; }
    }
}
