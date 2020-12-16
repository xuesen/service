using System;
using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Core.Model;

namespace IIMes.Services.Maintain.Repository
{
    public static class BomRepositoryExtension
    {
        public static SBom GetBom(this IRepository<SBom> repository, int bombaseid, string version)
        {
            return repository.Query().Where(p => p.SBomBase.Id == bombaseid && p.Version == version).FirstOrDefault();
        }
    }
}
