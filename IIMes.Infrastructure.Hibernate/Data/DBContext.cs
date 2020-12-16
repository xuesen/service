using IIMes.Infrastructure.Data.Interface;
using NHibernate;

namespace IIMes.Infrastructure.Hibernate.Data
{
    public class DBContext : IDBContext
    {
        private readonly ISession _session;

        public ISession Session
        {
            get
            {
                return _session;
            }
        }

        public DBContext(ISession session)
        {
            _session = session;
        }

        public IIMes.Infrastructure.Data.Interface.ITransaction BeginTransaction()
        {
            return new Transaction(_session.BeginTransaction());
        }

        public void Dispose()
        {
            _session.Dispose();
        }

        public void Flush()
        {
            _session.Flush();
        }
    }
}
