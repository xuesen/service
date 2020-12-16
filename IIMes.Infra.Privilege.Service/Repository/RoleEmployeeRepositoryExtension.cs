using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infra.Privilege.Model;

namespace IIMes.Infra.Privilege.Repository
{
    public static class RoleEmployeeRepositoryExtension
    {
        public static IQueryable<RoleEmployee> GetRoleEmployee(this IRepository<RoleEmployee> repository, int employeeid, string application)
        {
            return repository.Query().Where(p => p.SEmployee.Id == employeeid && p.SRole.Application == application);
        }

        public static RoleEmployee GetRoleEmployeeByRoleId(this IRepository<RoleEmployee> repository, int employeeid, int roleid)
        {
            return repository.Query().Where(p => p.SEmployee.Id == employeeid && p.SRole.Id == roleid).FirstOrDefault();
        }
    }
}
