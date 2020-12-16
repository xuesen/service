namespace IIMes.Infrastructure.Hibernate.Data
{
    public class Transaction : IIMes.Infrastructure.Data.Interface.ITransaction
    {
        private readonly NHibernate.ITransaction _tran;

        public Transaction(NHibernate.ITransaction tran)
        {
            _tran = tran;
        }

        public void Commit()
        {
            _tran.Commit();
        }

        public void Dispose()
        {
            _tran.Dispose();
        }

        public void Rollback()
        {
            _tran.Rollback();
        }
    }
}
