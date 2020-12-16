using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Core.Model;
using NHibernate.Linq;

namespace IIMes.Services.Maintain.Repository
{
    public static class SettingRepositoryExension
    {
         public static IQueryable<SSetting> Program(this IRepository<SSetting> repository, string program)
        {
            return repository.Query().Where(p => p.Program == program);
        }
    }
}
