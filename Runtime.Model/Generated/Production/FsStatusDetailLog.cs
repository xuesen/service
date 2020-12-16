using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.Production
{
    public class FsStatusDetailLog : DomainObject, IAggregateRoot
    {
        public virtual int? FsStatusLogId { get; set; }

        public virtual string Station { get; set; }

        public virtual string WorkOrder { get; set; }

        public virtual int ItemPartId { get; set; }

        public virtual int ScanPartId { get; set; }

        public virtual string ScanNo { get; set; }
    }
}
