using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Infra.Privilege.Model
{
    
    public partial class Employee : DomainObject , IAggregateRoot
    {
        public virtual Department SDepartment { get; set; }      
        public virtual Employee SEmployee { get; set; }
        public virtual string Code { get; set; }
        public virtual string Password { get; set; }
        public virtual string Name { get; set; }
        public virtual string Duty { get; set; }
        public virtual string Email { get; set; }
        public virtual string PhoneNo { get; set; }
        public virtual string Description { get; set; }
        public virtual string Type { get; set; }
        public virtual string Domain { get; set; }
        public virtual DateTime? LeaveTime { get; set; }
        public virtual string UpdateEditor { get; set; }
    }
}
