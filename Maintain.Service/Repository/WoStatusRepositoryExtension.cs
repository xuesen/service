using System;
using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Core.Model;

namespace IIMes.Services.Maintain.Repository
{
    public static class WoStatusRepositoryExtension
    {
        public static SWoStatus GetWoStaus(this IRepository<SWoStatus> repository, string name)
        {
            return repository.Query().Where(p => p.Name == name).FirstOrDefault();
        }

        public static SWoStatus GetWoStausByCode(this IRepository<SWoStatus> repository, string code)
        {
            return repository.Query().Where(p => p.Code == code).FirstOrDefault();
        }
    }
}
