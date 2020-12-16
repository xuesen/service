using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Core.Model;
using NHibernate.Linq;

namespace IIMes.Services.Maintain.Repository
{
    public static class ProductLineRepositoryExension
    {
        // public static IQueryable<SProductLine> GetPdLineByCode(this IRepository<SProductLine> repository, string code)
        // {
        //     return repository.Query().Where(p => p.Code == code);
        // }

        // public static IQueryable<SProductLine> GetPdLineByName(this IRepository<SProductLine> repository, string name)
        // {
        //     return repository.Query().Where(p => p.Name == name);
        // }

        // public static IQueryable<SProductLine> GetPdLineByDesc(this IRepository<SProductLine> repository, string desc)
        // {
        //     return repository.Query().Where(p => p.Description == desc);
        // }

        // public static IQueryable<SProductLine> GetPdLineByWorkShopId(this IRepository<SProductLine> repository, int workshopid)
        // {
        //     return repository.Query().Where(p => p.SWorkshop.Id == workshopid);
        // }
    }
}
