using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Runtime.Model.WOR;

namespace IIMes.Services.Runtime.Repository
{
    public static class WorInfoRepositoryExtension
    {
        public static IQueryable<WorInfo> GetWorInfo(this IRepository<WorInfo> repository, string wo)
        {
            return repository.Query().Where(p => p.WorkOrder == wo);
        }
    }
}
