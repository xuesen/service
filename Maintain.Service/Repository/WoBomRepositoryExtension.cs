using System.Collections.Generic;
using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Core.Model;

namespace IIMes.Services.Maintain.Repository
{
    public static class WoBomRepositoryExtension
    {
        public static IQueryable<WoBom> GetWoBomSubPart(this IRepository<WoBom> repository, string workorder)
        {
            return repository.Query().Where(p => p.WoBase.WorkOrder == workorder && p.SubPartGroup != "");
        }

        public static IQueryable<WoBom> GetWoBomNoSub(this IRepository<WoBom> repository, string workorder)
        {
            return repository.Query().Where(p => p.WoBase.WorkOrder == workorder && p.SubPartGroup == "");
        }

        public static IQueryable<WoBom> GetWoBom(this IRepository<WoBom> repository, string workorder)
        {
            return repository.Query().Where(p => p.WoBase.WorkOrder == workorder);
        }
    }
}
