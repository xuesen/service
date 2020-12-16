using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Runtime.Model.System;
using NHibernate.Linq;

namespace IIMes.Services.Runtime.Repository
{
    public static class SettingRepositoryExension
    {
         public static IQueryable<Setting> GetSettingByProgram(this IRepository<Setting> repository, string program)
        {
            return repository.Query().Where(p => p.Program == program);
        }
    }
}
