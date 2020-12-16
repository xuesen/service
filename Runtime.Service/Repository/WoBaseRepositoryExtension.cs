using System.Collections.Generic;
using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Runtime.Interface.DTO;
using IIMes.Services.Runtime.Model.Production;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace IIMes.Services.Runtime.Repository
{
    public static class WoBaseRepositoryExtension
    {
        public static WoBase GetWoBaseByWo(this IRepository<WoBase> repository, string wo)
        {
            return repository.Query().Where(p => p.WorkOrder == wo).ToList().FirstOrDefault();
        }

        public static WoBase GetWoBaseByWoStatus(this IRepository<WoBase> repository, string wo, int statusid)
        {
            return repository.Query().Where(p => p.WorkOrder == wo && p.StatusId == statusid).ToList().FirstOrDefault();
        }

        public static void UpdateWobaseStatusBywo(this IRepository<WoBase> repository, string wo, int statusid)
        {
            repository.Query().Where(p => p.WorkOrder == wo).Update(p => new WoBase
            {
                StatusId = statusid
            });
        }

        public static IQueryable<WoBase> QueryWos(this IRepository<WoBase> repository, IList<string> wostatuslst)
        {
            return repository.Query().Where(p => wostatuslst.Contains(p.WoStatus.Name));
        }        
    }
}
