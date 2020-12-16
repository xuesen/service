using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public partial class ReworkLog : DomainObject, IAggregateRoot
    {
        public virtual string ReworkLot { get; set; }

        public virtual string SerialNumber { get; set; }

        public virtual int CurrentProcessId { get; set; }

        public virtual string EmployeeCode { get; set; }

        public virtual DateTime CreateTime { get; set; }
    }
}
