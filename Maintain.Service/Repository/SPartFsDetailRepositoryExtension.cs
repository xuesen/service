using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Core.Model;

namespace IIMes.Services.Maintain.Repository
{
    public static class SPartFsDetailRepositoryExtension
    {
        public static IQueryable<SPartFsDetail> GetPartFsDetail(this IRepository<SPartFsDetail> repository, int partfsid)
        {
            return repository.Query().Where(p => p.PartFsId == partfsid);
        }
    }
}
