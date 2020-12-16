using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Core.Model;
using NHibernate.Linq;

namespace IIMes.Services.Maintain.Repository
{
    public static class WoFsSubRepositoryExtension
    {
        public static void UpdateWoFsSub(this IRepository<WoFsSub> repository, WoFsSub wofssub)
        {
            repository.Query().Where(p => p.WoFsDetailId == wofssub.WoFsDetailId).Update(p => new WoFsSub
            {
                SubPartId = wofssub.SubPartId
            });
        }

        public static IQueryable<WoFsSub> GetWoFsSub(this IRepository<WoFsSub> repository, int wofsdetailid)
        {
            return repository.Query().Where(p => p.WoFsDetailId == wofsdetailid);
        }

        public static void DeleteWoFsSub(this IRepository<WoFsSub> repository, int id)
        {
            repository.DeleteByLambda(p => p.Id == id);
        }
    }
}
