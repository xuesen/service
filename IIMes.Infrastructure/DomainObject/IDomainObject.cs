using System;

namespace IIMes.Infrastructure.DomainObject
{
    public interface IDomainObject<TKeyType>
    {
        TKeyType Id { get; set; }

        DateTime Cdt { get; set; }

        DateTime Udt { get; set; }

        string Editor { get; set; }
    }
}
