using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using IIMes.Infrastructure.Data.Interface;

namespace IIMes.Infrastructure.Aspect
{
    public class TransactionalAttribute : AbstractInterceptorAttribute
    {
        // private IDBContext _dbContext;

        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            var dbContext = context.ServiceProvider.GetService(typeof(IDBContext)) as IDBContext;
            using (var tran = dbContext.BeginTransaction())
            {
                try
                {
                    await next(context);
                    tran.Commit();
                }
                catch (System.Exception)
                {
                    try
                    {
                        tran.Rollback();
                    }
                    catch (System.Exception)
                    { }
                    throw;
                }
            }
        }
    }
}
