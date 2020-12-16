using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public class QcSampleResult : DomainObject, IAggregateRoot
    {
        public virtual string SerialNumber { get; set; }

        public virtual int SampleLogId { get; set; }

        public virtual int TestLogId { get; set; }

        public virtual int QcLotResultId { get; set; }

        public virtual DateTime CreateTime { get; set; }

        public virtual DateTime UpdateTime { get; set; }
    }
}
