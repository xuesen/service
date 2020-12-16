using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Runtime.Model.WOR;

namespace IIMes.Services.Runtime.Repository
{
    public static class WorRepairKeyPartsRepositoryExtension
    {
        public static IQueryable<WorRepairKeyparts> GetWorRepairKeyparts(this IRepository<WorRepairKeyparts> repository, string sn)
        {
            return repository.Query().Where(p => p.SerialNumber == sn);
        }
    }
}
