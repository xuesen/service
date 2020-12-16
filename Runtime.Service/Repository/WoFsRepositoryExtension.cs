using System.Collections.Generic;
using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Runtime.Model.Process;
using IIMes.Services.Runtime.Model.Product;
using IIMes.Services.Runtime.Model.Production;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace IIMes.Services.Runtime.Repository
{
    public static class WoFsRepositoryExtension
    {
        public static IQueryable<WoFs> GetWoFs(this IRepository<WoFs> repository, string wo)
        {
            return repository.Query().Where(p => p.WorkOrder == wo);
        }

        public static IQueryable<WoFs> GetWoFsByWoAndEq(this IRepository<WoFs> repository, string wo, int equipmentId )
        {
            return repository.Query().Where(p => p.WorkOrder == wo && p.EquipmentId == equipmentId);
        }
    }
}
