using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infra.Privilege.Model;

namespace IIMes.Infra.Privilege.Repository
{
    public static class ResourcePermissionRepositoryExtension
    {
        public static IQueryable<ResourcePermission> GetResourcePermission(this IRepository<ResourcePermission> repository, int resourceid)
        {
            return repository.Query().Where(p => p.SResource.Id == resourceid).OrderBy(p => p.Id);
        } 
    }
}
