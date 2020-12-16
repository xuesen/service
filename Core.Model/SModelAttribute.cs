using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public class SModelAttribute : DomainObject, IAggregateRoot
    {
        public virtual SModel SModel { get; set; }

        public virtual SAttribute SAttribute { get; set; }

        public virtual string Value { get; set; }
    }
}
