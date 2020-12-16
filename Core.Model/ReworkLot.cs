using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public partial class ReworkLot : DomainObject, IAggregateRoot
    {
        public virtual string Rework_Lot { get; set; }

        public virtual string UnbindSpec { get; set; }

        public virtual int ReworkWorkflowId { get; set; }

        public virtual int InputProcessId { get; set; }

        public virtual string Remark { get; set; }

        public virtual string EmployeeCode { get; set; }

        public virtual DateTime CreateTime { get; set; }

        public virtual int PartId { get; set; }

        public virtual string PartNo { get; set; }
    }
}
