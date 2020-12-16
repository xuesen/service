using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public class SPrintSetting : DomainObject, IAggregateRoot
    {
        public virtual int TerminalId { get; set; }

        public virtual string ClientServiceIp { get; set; }

        public virtual string ClientServicePort { get; set; }

        public virtual int PrintTemplateId { get; set; }

        public virtual string PrinterName { get; set; }

        public virtual int XOffSet { get; set; }

        public virtual int YOffSet { get; set; }

        public virtual string EmployeeCode { get; set; }

        public virtual DateTime CreateTime { get; set; }

        public virtual DateTime UpdateTime { get; set; }
    }
}
