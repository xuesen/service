using System;

namespace IIMes.Infrastructure.Data.Interface
{
    public interface IDBContext : IDisposable
    {
        void Flush();

        ITransaction BeginTransaction();
    }
}
