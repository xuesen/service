using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using IIMes.Infrastructure.Query;

namespace IIMes.Infrastructure.Data.Interface
{
    /// <summary>
    /// Generic Repository interface.
    /// </summary>
    public interface IRepository<T>
        where T : class
    {
        /// <summary>
        /// Get a aggregate root object with given identity.
        /// </summary>
        /// <param name="id">identity.</param>
        /// <returns>aggregate root object.</returns>
        T Get(object id);

        IList<T> FindAll();

        /// <summary>
        /// add a new aggregate root object.
        /// </summary>
        /// <param name="item">aggregete root.</param>
        /// <returns>identity.</returns>
        object Add(T item);

        /// <summary>
        /// add multiple aggregate root objects.
        /// </summary>
        /// <param name="items">objects to add.</param>
        void Add(IList<T> items);

        /// <summary>
        /// delete aggregate root object.
        /// </summary>
        /// <param name="item">object to delete.</param>
        void Delete(T item);

        /// <summary>
        /// delete by lamda.
        /// </summary>
        /// <param name="expression">condition expresion.</param>
        void DeleteByLambda(Expression<Func<T, bool>> expression);

        /// <summary>
        /// update an object with given value.
        /// </summary>
        /// <param name="item">object with given value.</param>
        /// <returns>updated object.</returns>
        T Update(T item);

        IQueryable<T> Query();

        IList<T> Query(QueryParameter query);

        IList<TM> ExecSqlQuery<TM>(string sql, params KeyValuePair<string, object>[] parameters);

        IList<TM> ExecNamedSqlQuery<TM>(string sqlName, int top, params KeyValuePair<string, object>[] parameters);

        IList<TM> ExecNamedSqlQuery<TM>(string sqlName, params KeyValuePair<string, object>[] parameters);

        // DataTable CallSPForQuery(string spName, params object[] commandParameters);
    }
}
