using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public partial class Material : DomainObject
    {
        public virtual string ReelId { get; set; }

        public virtual string Pn { get; set; }

        public virtual string MnfPn { get; set; }

        public virtual string MnfDate { get; set; }

        public virtual string Manufacturer { get; set; }

        public virtual string OrderId { get; set; }

        public virtual string Supplier { get; set; }

        public virtual DateTime ReceiveDate { get; set; }

        public virtual string LotNo { get; set; }

        public virtual int Amount { get; set; }

        public virtual int Type { get; set; }

        public virtual string Remark { get; set; }

        public virtual string Remark2 { get; set; }

        public virtual string Remark3 { get; set; }

        public virtual int? RohsStatus { get; set; }

        public virtual string EmployeeCode { get; set; }

        public virtual DateTime CreateTime { get; set; }

        public virtual DateTime UpdateTime { get; set; }
    }
}
