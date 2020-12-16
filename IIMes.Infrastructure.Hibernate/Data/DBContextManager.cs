using IIMes.Infrastructure.Data.Interface;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;

namespace IIMes.Infrastructure.Hibernate.Data
{
    public class DBContextManager : IDBContextManager
    {
        private readonly ISessionFactory _sessionFactory;

        public DBContextManager()
        {
            var nhConfig = new Configuration().Configure();
            nhConfig.SetInterceptor(new DataInterceptor());
            _sessionFactory = nhConfig.BuildSessionFactory();
        }

        public IDBContext BindDBContext(IDBContext context)
        {
            var old = CurrentSessionContext.Unbind(_sessionFactory);
            CurrentSessionContext.Bind(((DBContext)context).Session);
            return new DBContext(old);
        }

        public void CloseDBContext(IDBContext context)
        {
            ((DBContext)context).Session.Close();
            ((DBContext)context).Session.Dispose();
        }

        public IDBContext GetCurrentDBContext()
        {
            try
            {
                var session = _sessionFactory.GetCurrentSession();
                return new DBContext(session);
            }
            catch (HibernateException)
            {
                return null;
            }
        }

        public IDBContext OpenDBContext()
        {
            var session = _sessionFactory.OpenSession();
            return new DBContext(session);
        }

        public IDBContext UnbindContext()
        {
            var old = CurrentSessionContext.Unbind(_sessionFactory);
            if (old != null)
            {
                return new DBContext(old);
            }
            else
            {
                return null;
            }
        }

        public IDBContext UnbindDBContext()
        {
            var session = CurrentSessionContext.Unbind(_sessionFactory);
            if (session != null)
            {
                return new DBContext(session);
            }

            return null;
        }
    }
}
