using System.Collections.Generic;
using AutoMapper;
using IIMes.Infrastructure.Aspect;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Query;

namespace IIMes.Infrastructure.Service
{
    public class BaseService<T, TDTO> : IService<T, TDTO>
        where T : DomainObject.DomainObject
        where TDTO : class
    {
        private readonly IRepository<T> rep;
        private readonly IMapper mapper;

        protected IRepository<T> Rep => rep;

        protected IMapper Mapper => mapper;

        public BaseService(IRepository<T> rep, IMapper mapper)
        {
            this.rep = rep;
            this.mapper = mapper;
        }

        public virtual TDTO FindById(int id)
        {
            var x = Rep.Get(id);
            var y = mapper.Map<T, TDTO>(x);
            return y;
        }

        public virtual IEnumerable<TDTO> GetAll()
        {
            var x = Rep.FindAll();
            var y = mapper.Map<IList<T>, IEnumerable<TDTO>>(x);
            return y;
        }

        public virtual IEnumerable<TDTO> Query(QueryParameter query)
        {
            var x = Rep.Query(query);
            var y = mapper.Map<IList<T>, IEnumerable<TDTO>>(x);
            return y;
        }

        public virtual void Add(TDTO obj)
        {
            var y = mapper.Map<TDTO, T>(obj);
            Rep.Add(y);
        }

        public virtual void AddList(IList<TDTO> obj)
        {
            for (int i = 0; i < obj.Count; i++)
            {
                var y = mapper.Map<TDTO, T>(obj[i]);
                Rep.Add(y);
            }
        }

        public virtual void Update(int id, TDTO obj)
        {
            var y = mapper.Map<TDTO, T>(obj);
            y.Id = id;
            Rep.Update(y);
        }

        public virtual void Delete(int id)
        {
            Rep.DeleteByLambda(o => o.Id == id);
        }
    }
}