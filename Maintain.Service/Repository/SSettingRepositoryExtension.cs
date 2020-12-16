using System;
using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Core.Model;

namespace IIMes.Services.Maintain.Repository
{
    public static class SSettingRepositoryExtension
    {
        public static IQueryable<SSetting> GetSetting(this IRepository<SSetting> repository, string program)
        {
            return repository.Query().Where(p => p.Program == program).OrderBy(p => p.Name);
        }
    }
}
