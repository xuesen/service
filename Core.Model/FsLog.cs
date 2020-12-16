using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public partial class FsLog : DomainObject
    {
        public virtual string Station { get; set; }

        public virtual string Side { get; set; }

        public virtual int? EquipmentId { get; set; }

        public virtual int FsAction { get; set; }

        public virtual int PartId { get; set; }

        public virtual int ItemPartId { get; set; }

        public virtual int ScanPartId { get; set; }

        public virtual string ScanNo { get; set; }

        public virtual string FeederNo { get; set; }

        public virtual string EmployeeCode { get; set; }

        public virtual DateTime CreateTime { get; set; }
    }
}