using System;
using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Core.Model;

namespace IIMes.Services.Maintain.Repository
{
    public static class BomItemRepositoryExtension
    {
        public static SBomItem GetBomItem(this IRepository<SBomItem> repository, int itempartid, int bomid)
        {
            return repository.Query().Where(p => p.SPart.Id == itempartid && p.SBom.Id == bomid).FirstOrDefault();
        }

        public static IQueryable<SBomItem> GetBomItemByGroup(this IRepository<SBomItem> repository, string group, int bomid)
        {
            return repository.Query().Where(p => p.ItemGroup == group && p.SBom.Id == bomid);
        }
    }
}
