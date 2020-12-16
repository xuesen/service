using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public partial class PackRemove : DomainObject, IAggregateRoot
    {
        public virtual string Type { get; set; }

        public virtual string RemoveNo { get; set; }

        public virtual string Remark { get; set; }

        public virtual string EmployeeCode { get; set; }

        public virtual DateTime CreateTime { get; set; }
    }
}
