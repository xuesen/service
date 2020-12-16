using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public class QcTestitemResult : DomainObject, IAggregateRoot
    {
        public virtual string SerialNumber { get; set; }

        public virtual int? ProductLogId { get; set; }

        public virtual int PartProcessTestitemId { get; set; }

        public virtual string InspectValue { get; set; }

        public virtual bool InspectResult { get; set; }

        public virtual string EmployeeCode { get; set; }

        public virtual DateTime? CreateTime { get; set; }
    }
}
