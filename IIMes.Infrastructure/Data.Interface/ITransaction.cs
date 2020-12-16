using System;

namespace IIMes.Infrastructure.Data.Interface
{
    public interface ITransaction : IDisposable
    {
        void Commit();

        void Rollback();
    }
}
