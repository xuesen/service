using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infra.Privilege.Model;
using NHibernate.Linq;
using System.Collections.Generic;

namespace IIMes.Infra.Privilege.Repository
{
    public static class EmployeeRepositoryExtension
    {
        public static Employee GetEmployee(this IRepository<Employee> repository, string username, string pwd)
        {
            return repository.Query().Where(p => p.Code == username && p.Password == pwd).FirstOrDefault();
        }
        public static IQueryable<Employee> GetEmployeeByName(this IRepository<Employee> repository, string name)
        {
            return repository.Query().Where(p => p.Name.Like("%" + name + "%"));
        }

        public static IQueryable<Employee> GetEmployeeByEmpId(this IRepository<Employee> repository, IList<int> empidlst)
        {
            return repository.Query().Where(p => empidlst.Contains(p.SEmployee.Id));
        } 
    }
}
