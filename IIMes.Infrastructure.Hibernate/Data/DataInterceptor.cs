using System;
using System.Collections;
using NHibernate;
using NHibernate.SqlCommand;
using NHibernate.Type;

namespace IIMes.Infrastructure.Hibernate.Data
{
    public class DataInterceptor : IInterceptor
    {
        public void AfterTransactionCompletion(ITransaction tx)
        {
        }

        /// <summary>
        /// Called when a NHibernate transaction is begun via the NHibernate (see cref="T:NHibernate.ITransaction")
        ///             API. Will not be called if transactions are being controlled via some other mechanism.
        /// </summary>
        /// <param name="tx">transaction.</param>
        public void AfterTransactionBegin(ITransaction tx)
        {
        }

        public void BeforeTransactionCompletion(ITransaction tx)
        {
        }

        public int[] FindDirty(
            object entity,
            object id,
            object[] currentState,
            object[] previousState,
            string[] propertyNames,
            IType[] types)
        {
            return null;
        }

        public object Instantiate(string entityName, object id)
        {
            return null;
        }

        public object GetEntity(string entityName, object id)
        {
            return null;
        }

        public string GetEntityName(object entity)
        {
            return null;
        }

        public object Instantiate(string entityName, EntityMode entityMode, object id)
        {
            return null;
        }

        public bool? IsTransient(object entity)
        {
            return null;
        }

        public void OnCollectionRecreate(object collection, object key)
        {
        }

        public void OnCollectionRemove(object collection, object key)
        {
        }

        public void OnCollectionUpdate(object collection, object key)
        {
        }

        public void OnDelete(object entity, object id, object[] state, string[] propertyNames, IType[] types)
        {
        }

        public bool OnFlushDirty(
            object entity,
            object id,
            object[] currentState,
            object[] previousState,
            string[] propertyNames,
            IType[] types)
        {
            bool modified = false;

            for (int i = 0; i < propertyNames.Length; ++i)
            {
                if (propertyNames[i] == "Udt")
                {
                    DateTime datatime = DateTime.Now;
                    if ((DateTime)currentState[i] == DateTime.MinValue)
                    {
                        currentState[i] = datatime;
                        modified = true;
                    }
                }
            }

            return modified;
        }

        public bool OnLoad(object entity, object id, object[] state, string[] propertyNames, IType[] types)
        {
            bool modified = false;

            for (int i = 0; i < propertyNames.Length; ++i)
            {
                if (types[i].ReturnedClass == typeof(string))
                {
                    if (state[i] != null && state[i] is string)
                    {
                        state[i] = ((string)state[i]).Trim();
                        modified = true;
                    }
                }
            }

            return modified;
        }

        public SqlString OnPrepareStatement(SqlString sql)
        {
            return sql;
        }

        public bool OnSave(object entity, object id, object[] state, string[] propertyNames, IType[] types)
        {
            bool modified = false;

            DateTime datetime = DateTime.Now;
            for (int i = 0; i < propertyNames.Length; ++i)
            {
                if ((propertyNames[i] == "Udt" || propertyNames[i] == "Cdt") && ((state[i] is DateTime && (DateTime)state[i] == DateTime.MinValue) || (state[i] is DateTime && ((DateTime?)state[i] == null || ((DateTime?)state[i]).Value == DateTime.MinValue))))
                {
                    state[i] = datetime;
                    modified = true;
                }
            }

            return modified;
        }

        public void PostFlush(ICollection entities)
        {
        }

        public void PreFlush(ICollection entities)
        {
        }

        public void SetSession(ISession session)
        {
        }
    }
}
