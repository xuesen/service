using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Runtime.Model.Product;

namespace IIMes.Services.Runtime.Repository
{
    public static class MaterialRepositoryExtension
    {
        public static IQueryable<Material> GetPartLstByReel(this IRepository<Material> repository, string reelid)
        {
            return repository.Query().Where(p => p.ReelId == reelid);
        }

        public static Material GetPartByReel(this IRepository<Material> repository, string reelid)
        {
            return repository.Query().Where(p => p.ReelId == reelid).FirstOrDefault();
        }

        public static IQueryable<Material> GetPartLstByPart(this IRepository<Material> repository, string partno)
        {
            return repository.Query().Where(p => p.Pn == partno);
        }

        public static Material GetPartByPart(this IRepository<Material> repository, string partno)
        {
            return repository.Query().Where(p => p.Pn == partno).FirstOrDefault();
        }
    }
}
