using System.Collections.Generic;
using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Runtime.Interface.DTO;
using IIMes.Services.Runtime.Model.Production;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace IIMes.Services.Runtime.Repository
{
    public static class WoStatusRepositoryExtension
    {
        public static WoStatus GetStatusidByName(this IRepository<WoStatus> repository, string name)
        {
            return repository.Query().Where(p => p.Name == name).ToList().FirstOrDefault();
        }
    }
}
