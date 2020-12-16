using System;
using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Core.Model;

namespace IIMes.Services.Maintain.Repository
{
    public static class BomProcessBomRepositoryExtension
    {
        public static IQueryable<SBopProcessBom> GetBomProcessBom(this IRepository<SBopProcessBom> repository, int bopbaseid)
        {
            return repository.Query().Where(p => p.BopBase.Id == bopbaseid);
        }
    }
}
