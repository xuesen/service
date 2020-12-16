using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Exception;
using IIMes.Infrastructure.Hibernate.Data;
using IIMes.Infrastructure.Hibernate.Exception;
using Microsoft.Extensions.DependencyInjection;

namespace IIMes.Services.Runtime.Api
{
    public static class NHibernateExtensions
    {
        public static IServiceCollection AddNHibernate(this IServiceCollection services)
        {
            var dbContextManager = new DBContextManager();
            services.AddSingleton<IDBContextManager>(dbContextManager);
            services.AddScoped<IDBContext>(factory => dbContextManager.OpenDBContext());
            return services;
        }
    }
}
