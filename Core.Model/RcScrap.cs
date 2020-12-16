using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public partial class RcScrap : DomainObject
    {
        public virtual string RcNo { get; set; }

        public virtual int Qty { get; set; }

        public virtual int? TerminalId { get; set; }

        public virtual string TerminalCode { get; set; }

        public virtual int ProcessId { get; set; }

        public virtual string ProcessCode { get; set; }

        public virtual string ScrapType { get; set; }

        public virtual string ScrapMemo { get; set; }

        public virtual string EmployeeCode { get; set; }

        public virtual DateTime CreateTime { get; set; }
    }
}
