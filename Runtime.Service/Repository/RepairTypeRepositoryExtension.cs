using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Runtime.Model.Quality;
using IIMes.Services.Runtime.Model.Resource;

namespace IIMes.Services.Runtime.Repository
{
    public static class RepairTypeRepositoryExtension
    {
        public static RepairType GetRepairType(this IRepository<RepairType> repository, string code)
        {
            return repository.Query().Where(p => p.Code == code).FirstOrDefault();
        }
    }
}
