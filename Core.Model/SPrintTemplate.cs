using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public class SPrintTemplate : DomainObject, IAggregateRoot
    {
        public virtual string Name { get; set; }

        public virtual string FileString { get; set; }

        public virtual string Description { get; set; }

        public virtual string SpName { get; set; }

        public virtual int Rotate180 { get; set; }

        public virtual int Layout { get; set; }

        public virtual bool Collate { get; set; }

        public virtual int PrintMode { get; set; }

        public virtual int Continuous { get; set; }

        public virtual int PrintKey { get; set; }

        public virtual string Remark { get; set; }

        public virtual SSetting SSetting { get; set; }
    }
}
