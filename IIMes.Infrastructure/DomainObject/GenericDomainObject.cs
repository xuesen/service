using System;

namespace IIMes.Infrastructure.DomainObject
{
    public abstract class GenericDomainObject<TKey> : IDomainObject<TKey>, IAggregateRoot
    {
        public virtual TKey Id { get; set; }

        public virtual DateTime Cdt { get; set; }

        public virtual DateTime Udt { get; set; }

        public virtual string Editor { get; set; }
    }
}
