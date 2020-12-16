using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public class SEmployee : DomainObject, IAggregateRoot
    {
        public virtual string Code { get; set; }

        public virtual string Password { get; set; }

        public virtual string Name { get; set; }

        public virtual string Duty { get; set; }

        public virtual string Email { get; set; }

        public virtual string PhoneNo { get; set; }

        public virtual string Description { get; set; }

        public virtual string Type { get; set; }

        public virtual string Domain { get; set; }

        public virtual DateTime? Leave_office_time { get; set; }

        public virtual SDepartment SDepartment { get; set; }

        public virtual SEmployee Employee { get; set; }
    }
}
