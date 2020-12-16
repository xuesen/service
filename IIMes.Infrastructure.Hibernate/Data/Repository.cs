using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Query;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Transform;

namespace IIMes.Infrastructure.Hibernate.Data
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        private readonly IDBContext _context;

        public Repository(IDBContext context)
        {
            _context = context;
        }

        public ISession CurrentSession
        {
            get { return ((DBContext)_context).Session; }
        }

        public T Get(object id)
        {
            return CurrentSession.Get<T>(id);
        }

        public object Add(T item)
        {
            return CurrentSession.Save(item);
        }

        public void Add(IList<T> items)
        {
            if (items == null)
            {
                return;
            }

            var i = 0;
            foreach (var item in items)
            {
                CurrentSession.Save(item);
                // 20, same as the ADO batch size
                if (i % 50 == 0)
                {
                    // flush a batch of inserts and release memory:
                    CurrentSession.Flush();
                    CurrentSession.Clear();
                }

                i++;
            }
        }

        public T Update(T item)
        {
            return CurrentSession.Merge(item);
        }

        public void Delete(T item)
        {
            CurrentSession.Delete(item);
        }

        public void DeleteByLambda(Expression<Func<T, bool>> expression)
        {
            CurrentSession.Query<T>().Where(expression).Delete();
        }

        public IList<TM> ExecNamedSqlQuery<TM>(string sqlName, int top, params KeyValuePair<string, object>[] parameters)
        {
            var pre = CurrentSession.GetNamedQuery(sqlName);
            if (parameters != null && parameters.Length > 0)
            {
                foreach (var p in parameters)
                {
                    if (p.Value is IEnumerable<string> || p.Value is IEnumerable<int>)
                        pre = pre.SetParameterList(p.Key, (IEnumerable)p.Value);
                    else
                        pre = pre.SetParameter(p.Key, p.Value);
                }
            }

            if (top > 0)
            {
                pre = pre.SetMaxResults(top);
            }

            if (typeof(TM) == typeof(Hashtable))
                return (IList<TM>)pre.SetResultTransformer(Transformers.AliasToEntityMap).List<Hashtable>();
            else if (typeof(TM).IsValueType || typeof(TM) == typeof(string) || typeof(TM) == typeof(byte[]))
                return pre.SetResultTransformer(Transformers.RootEntity).List<TM>();
            else
                return pre.SetResultTransformer(Transformers.AliasToBean<TM>()).List<TM>();
        }

        public IList<TM> ExecNamedSqlQuery<TM>(string sqlName, params KeyValuePair<string, object>[] parameters)
        {
            return ExecNamedSqlQuery<TM>(sqlName, 0, parameters);
        }

        public IQueryable<T> Query()
        {
            return CurrentSession.Query<T>();
        }

        public IList<T> Query(QueryParameter query)
        {
            // var queryProperty = typeof(T).GetProperty(query.Name);
            var param = Expression.Parameter(typeof(T), "item");
            Expression body = param;
            foreach (var member in query.Name.Split('.'))
            {
                body = Expression.PropertyOrField(body, member);
            }

            Expression expression = GetExpression(body, query.Value, query.Fuzzy);
            var lambda = Expression
              .Lambda<Func<T, bool>>(expression, param);
            var result = CurrentSession.Query<T>().Where(lambda).ToList().AsQueryable();

            // Order by.
            if (query.OrderBy != string.Empty)
            {
                if (result.PropertyExists(query.OrderBy))
                {
                    result = result.OrderByProperty(query.OrderBy);
                }
            }

            return result.ToList();
        }

        // private string GetOrderbyPropertyName(Type t, string queryPropertyName)
        // {
        //     // 如果T中存在Name属性则按Name排序.
        //     // 否则，如果存在queryPropertyName属性则按queryPropertyName排序.
        //     var nameProperty = t.GetProperty("Name");
        //     if (nameProperty != null)
        //     {
        //         return "Name";
        //     }
        //     var queryProperty = t.GetProperty(queryPropertyName);
        //     if (queryProperty != null)
        //     {
        //         return queryPropertyName;
        //     }
        //     return string.Empty;
        // }

        private Expression GetExpression(Expression member, object value, bool fuzzy)
        {
            Expression ret = null;
            // var tmpValue = Activator.CreateInstance(member.Type);
            var tmpValue = Convert.ChangeType(value, member.Type);

            var searchExpression = Expression.Constant(tmpValue, member.Type);
            if (member.Type == typeof(string) && fuzzy)
            {
                var method = typeof(string)
                  .GetMethod("Contains", new[] { typeof(string) });
                ret = Expression
                .Call(member, method, searchExpression);
            }
            else
            {
                ret = Expression.Equal(member, searchExpression);
            }

            return ret;
        }

        // private MethodInfo GetMethod(Type type)
        // {
        //     if (type == typeof(int))
        //     {
        //         return typeof(int)
        //           .GetMethod("Equals", new[] { typeof(int) });
        //     }
        //     if (type == typeof(Int64))
        //     {
        //         return typeof(Int64)
        //           .GetMethod("Equals", new[] { typeof(Int64) });
        //     }
        //     return typeof(string)
        //       .GetMethod("Contains", new[] { typeof(string) });
        // }

        public IList<TM> ExecSqlQuery<TM>(string sql, params KeyValuePair<string, object>[] parameters)
        {
            IQuery pre = CurrentSession.CreateSQLQuery(sql);
            if (parameters != null && parameters.Length > 0)
            {
                foreach (var p in parameters)
                {
                    if (p.Value is IEnumerable<string> || p.Value is IEnumerable<int>)
                        pre = pre.SetParameterList(p.Key, (IEnumerable<string>)p.Value);
                    else
                        pre = pre.SetParameter(p.Key, p.Value);
                }
            }

            if (typeof(TM) == typeof(Hashtable))
                return (IList<TM>)pre.SetResultTransformer(Transformers.AliasToEntityMap).List<Hashtable>();
            else if (typeof(TM).IsValueType || typeof(TM) == typeof(string))
                return pre.SetResultTransformer(Transformers.RootEntity).List<TM>();
            else
                return pre.SetResultTransformer(Transformers.AliasToBean<TM>()).List<TM>();
        }

        public IList<T> FindAll()
        {
            return CurrentSession.QueryOver<T>().List();
        }
    }
}
