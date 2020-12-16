using System.Collections.Generic;
using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Runtime.Model.Product;
using IIMes.Services.Runtime.Model.Production;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace IIMes.Services.Runtime.Repository
{
    public static class WoBomRepositoryExtension
    {
        public static IQueryable<WoBom> GetWoBoms(this IRepository<WoBom> repository, int woBaseId)
        {
            return repository.Query().Where(p => p.WoBaseId == woBaseId);
        }        
    }
}
