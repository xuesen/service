using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.Print
{
    public class PrintSetting : DomainObject, IAggregateRoot
    {
        public virtual int TerminalId { get; set; }

        public virtual string ClientServiceIp { get; set; }

        public virtual string ClientServicePort { get; set; }

        public virtual int PrintTemplateId { get; set; }

        public virtual string PrinterName { get; set; }

        public virtual int XOffSet { get; set; }

        public virtual int YOffSet { get; set; }
    }
}
