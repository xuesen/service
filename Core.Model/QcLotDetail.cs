using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public class QcLotDetail : DomainObject, IAggregateRoot
    {
        public virtual string LotNubmber { get; set; }

        public virtual string UnitType { get; set; }

        public virtual int UnitQty { get; set; }

        public virtual string UnitNubmber { get; set; }

        public virtual int QcLotResultId { get; set; }

        public virtual DateTime CreateTime { get; set; }
    }
}
