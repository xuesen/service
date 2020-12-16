using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infra.Privilege.Model;

namespace IIMes.Infra.Privilege.Repository
{
    public static class ResourceRepositoryExtension
    {
        public static IQueryable<Resource> GetResource(this IRepository<Resource> repository, string application)
        {
            return repository.Query().Where(p => p.Application == application && p.IsLeaf == true).OrderBy(p => p.Id);
        }
        public static IQueryable<Resource> GetResourceByParentId(this IRepository<Resource> repository, int parentid)
        {
            return repository.Query().Where(p => p.SResource.Id == parentid).OrderBy(p => p.Id);
        }
        public static IQueryable<Resource> GetRootResource(this IRepository<Resource> repository, string application)
        {
            return repository.Query().Where(p => p.Application == application && p.SResource == null).OrderBy(p => p.Id);
        }
        public static IQueryable<Resource> GetRootResource4Login(this IRepository<Resource> repository, string application, string subapplication)
        {
            return repository.Query().Where(p => p.Application == application && p.SubApplication == subapplication && p.SResource == null).OrderBy(p => p.Id);
        }
        public static Resource GetResourceById(this IRepository<Resource> repository, string application, string subapplication, int id)
        {
            return repository.Query().Where(p => p.Application == application && p.SubApplication == subapplication && p.Id == id).FirstOrDefault();
        }
    }
}
