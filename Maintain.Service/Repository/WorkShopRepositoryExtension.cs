using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Core.Model;
using NHibernate.Linq;

namespace IIMes.Services.Maintain.Repository
{
    public static class WorkShopRepositoryExtension
    {
        // public static IQueryable<SWorkshop> GetWorkshop(this IRepository<SWorkshop> repository, int factoryid)
        // {
        //     return repository.Query().Where(p => p.SFactory.Id == factoryid);
        // }

        // public static IQueryable<SWorkshop> GetWorkshopByName(this IRepository<SWorkshop> repository, string name)
        // {
        //     return repository.Query().Where(p => p.Name == name);
        // }
    }
}
