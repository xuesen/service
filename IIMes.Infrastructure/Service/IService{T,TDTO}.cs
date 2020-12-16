using System.Collections.Generic;
using IIMes.Infrastructure.Query;

namespace IIMes.Infrastructure.Service
{
    public interface IService<T, TDTO>
        where T : DomainObject.DomainObject
        where TDTO : class
    {
        TDTO FindById(int id);

        IEnumerable<TDTO> GetAll();

        IEnumerable<TDTO> Query(QueryParameter query);

        void Add(TDTO obj);

        void AddList(IList<TDTO> obj);

        void Update(int id, TDTO obj);

        void Delete(int id);
    }
}