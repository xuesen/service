using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infra.Privilege.Model;

namespace IIMes.Infra.Privilege.Repository
{
    public static class RoleResourcePermissionRepositoryExtension
    {
        public static IQueryable<RoleResourcePermission> GetRoleResourcePermission(this IRepository<RoleResourcePermission> repository, int roleid)
        {
            return repository.Query().Where(p => p.SRole.Id == roleid);
        }

        public static void DeleteByRoleId(this IRepository<RoleResourcePermission> repository, int roleid)
        {
            repository.DeleteByLambda(p => p.SRole.Id == roleid);
        }
    }
}
