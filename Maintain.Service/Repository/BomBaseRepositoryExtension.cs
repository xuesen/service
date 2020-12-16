using System;
using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Core.Model;

namespace IIMes.Services.Maintain.Repository
{
    public static class BomBaseRepositoryExtension
    {
        public static SBomBase GetBomBase(this IRepository<SBomBase> repository, int partid)
        {
            return repository.Query().Where(p => p.SPart.Id == partid).FirstOrDefault();
        }
    }
}
