using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.WOR {
    
    public partial class WorRepairLogDetail : DomainObject, IAggregateRoot
    {
        public virtual int WorRepairLogId { get; set; }

        public virtual int DefectId { get; set; }

        public virtual string DefectCode { get; set; }

        public virtual int CauseId { get; set; }

        public virtual string CauseCode { get; set; }

        public virtual int RepairtypeId { get; set; }

        public virtual string RepairtypeCode { get; set; }

        public virtual int? DutyId { get; set; }

        public virtual string DutyCode { get; set; }

        public virtual string Remark { get; set; }

        public virtual string Location { get; set; }
    }
}
