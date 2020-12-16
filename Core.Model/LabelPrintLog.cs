using System;
using System.Collections.Generic;
using System.Text;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public partial class LabelPrintLog : DomainObject, IAggregateRoot
    {
        public virtual string PrintType { get; set; }

        public virtual string PrintLabelType { get; set; }

        public virtual string PrintNum { get; set; }

        public virtual int PrintTemplateId { get; set; }

        public virtual string ReprintReason { get; set; }

        public virtual int TerminalId { get; set; }

        public virtual string TerminalCode { get; set; }

        // public virtual int EmployeeId { get; set; }

        public virtual string EmployeeCode { get; set; }

        public virtual DateTime CreateTime { get; set; }
    }
}