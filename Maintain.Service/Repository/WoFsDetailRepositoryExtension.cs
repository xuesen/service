using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Core.Model;
using NHibernate.Linq;

namespace IIMes.Services.Maintain.Repository
{
    public static class WoFsDetailRepositoryExtension
    {
        public static void UpdateWoFsDetail(this IRepository<WoFsDetail> repository, WoFsDetail wofsdetail)
        {
            repository.Query().Where(p => p.Id == wofsdetail.Id).Update(p => new WoFsDetail
            {
                WoFsId = wofsdetail.WoFsId,
                Station = wofsdetail.Station,
                ItemPartId = wofsdetail.ItemPartId,
                ItemCount = wofsdetail.ItemCount,
                Location = wofsdetail.Location,
                FeederType = wofsdetail.FeederType,
                FeederSpec = wofsdetail.FeederSpec
            });
        }

        public static IQueryable<WoFsDetail> GetWoFsDetail(this IRepository<WoFsDetail> repository, int wofsno)
        {
            return repository.Query().Where(p => p.WoFsId == wofsno.ToString());
        }

        public static WoFsDetail GetWoFsByStationItemPartId(this IRepository<WoFsDetail> repository, string station, int itempartid)
        {
            return repository.Query().Where(p => p.Station == station && p.ItemPartId == itempartid).FirstOrDefault();
        }

        public static void DeleteWoFsDetail(this IRepository<WoFsDetail> repository, int id)
        {
            repository.DeleteByLambda(p => p.Id == id);
        }
    }
}
