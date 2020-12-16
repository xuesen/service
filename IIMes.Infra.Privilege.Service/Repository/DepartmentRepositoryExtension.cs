using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infra.Privilege.Model;
using System.Collections.Generic;
using NHibernate.Linq;

namespace IIMes.Infra.Privilege.Repository
{
    public static class DepartmentRepositoryExtension
    {
        public static IQueryable<Department> GetDepartment(this IRepository<Department> repository, string departmentname)
        {
            return repository.Query().Where(p => p.Name.Like("%" + departmentname + "%"));
        }

        public static IQueryable<Department> GetDepartmentByDepId(this IRepository<Department> repository, IList<int> depidlst)
        {
            return repository.Query().Where(p => depidlst.Contains(p.SDepartment.Id));
        }        
    }
}
