using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model
{
    public partial class QcDefect : DomainObject, IAggregateRoot
    {
        public new virtual int? Id { get; set; }

        public virtual string SerialNumber { get; set; }

        public virtual int TestProductLogId { get; set; }

        public virtual string DefectCode { get; set; }

        public virtual string ProcessCode { get; set; }

        public virtual string Remark { get; set; }
    }
}
