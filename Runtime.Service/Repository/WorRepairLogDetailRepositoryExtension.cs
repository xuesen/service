using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Runtime.Model.WOR;

namespace IIMes.Services.Runtime.Repository
{
    public static class WorRepairLogDetailRepositoryExtension
    {
        public static IQueryable<WorRepairLogDetail> GetRepairLogDetail(this IRepository<WorRepairLogDetail> repository, int repairlogid)
        {
            return repository.Query().Where(p => p.WorRepairLogId == repairlogid);
        }
    }
}
