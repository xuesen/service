using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public class WoFsDetail : DomainObject
    {
        public virtual string WoFsId { get; set; }

        public virtual string Station { get; set; }

        public virtual int ItemPartId { get; set; }

        public virtual int ItemCount { get; set; }

        public virtual string Location { get; set; }

        public virtual string FeederType { get; set; }

        public virtual string FeederSpec { get; set; }
    }
}
