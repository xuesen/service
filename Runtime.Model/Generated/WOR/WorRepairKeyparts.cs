using IIMes.Infrastructure.DomainObject;


namespace IIMes.Services.Runtime.Model.WOR {
    
    public partial class WorRepairKeyparts : DomainObject, IAggregateRoot
    {       
        public virtual string WorkOrder { get; set; }

        public virtual string SerialNumber { get; set; }

        public virtual int? RepairLogId { get; set; }

        public virtual int OldPartId { get; set; }

        public virtual string OldPartNo { get; set; }

        public virtual int NewPartId { get; set; }

        public virtual string NewPartNo { get; set; }

        public virtual string NewPartSn { get; set; }
        
        public virtual string Remark { get; set; }
    }
}
