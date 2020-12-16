using System;
using System.Collections.Generic;
using AutoMapper;
using IIMes.Infrastructure.Aspect;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Exception;
using IIMes.Infrastructure.Query;

namespace IIMes.Infrastructure.Service
{
    public class BaseMaintainService<T, TDTO> : BaseService<T, TDTO>
        where T : DomainObject.DomainObject
        where TDTO : class
    {
        private const string BizKeyName = "Code";
        private const string DeleteRefereceConflictMessage = "The DELETE statement conflicted with the REFERENCE constraint";

        public BaseMaintainService(IRepository<T> rep, IMapper mapper)
            : base(rep, mapper)
        {
        }

        public override void Add(TDTO obj)
        {
            CheckDuplication(obj);
            base.Add(obj);
        }

        public override void AddList(IList<TDTO> obj)
        {
            for (int i = 0; i < obj.Count; i++)
            {
                CheckDuplication(obj[i]);
            }
            base.AddList(obj);
        }

        [Transactional]
        public override void Update(int id, TDTO obj)
        {
            base.Update(id, obj);
        }

        public override void Delete(int id)
        {
            try
            {
                base.Delete(id);
            }
            catch (System.Exception e)
            {
                if (e.ToString().Contains(DeleteRefereceConflictMessage))
                {
                    throw new BizException("MTN002");
                }

                throw;
            }
        }

        protected virtual void CheckDuplication(TDTO obj)
        {
            var t = typeof(T);
            var keyProperty = t.GetProperty(BizKeyName);
            if (keyProperty != null)
            {
                var domain = Mapper.Map<TDTO, T>(obj);
                var keyValue = keyProperty.GetValue(domain);
                var duplications = Rep.Query(new QueryParameter() { Name = BizKeyName, Value = keyValue, Fuzzy = false });
                if (duplications != null && duplications.Count > 0)
                {
                    throw new BizException("MTN001", keyValue);
                }
            }
        }
    }
}