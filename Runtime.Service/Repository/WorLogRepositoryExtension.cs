using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Runtime.Model.WOR;

namespace IIMes.Services.Runtime.Repository
{
    public static class WorLogRepositoryExtension
    {
        public static WorLog GetWorLog(this IRepository<WorLog> repository, string sn)
        {
            return repository.Query().Where(p => p.SerialNumber == sn).OrderByDescending(p => p.Cdt).FirstOrDefault();
        }
    }
}
