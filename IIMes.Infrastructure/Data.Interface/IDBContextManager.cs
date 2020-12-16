using System;

namespace IIMes.Infrastructure.Data.Interface
{
    public interface IDBContextManager
    {
        IDBContext BindDBContext(IDBContext context);

        IDBContext UnbindDBContext();

        IDBContext OpenDBContext();

        void CloseDBContext(IDBContext context);

        IDBContext GetCurrentDBContext();
    }
}
