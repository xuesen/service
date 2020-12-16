using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Runtime.Model.Process;
using NHibernate.Linq;
using System.Collections.Generic;

namespace IIMes.Services.Runtime.Repository
{
    public static class BopProcessBomRepositoryExtension
    {
        public static IList<BopProcessBom> GetBopProcessBom(this IRepository<BopProcessBom> repository, int bopBaseId, int processId)
        {
            return repository.Query().Where(p => p.BopBaseId == bopBaseId && p.ProcessId == processId).ToList();
        }

        public static IList<BopProcessBom> GetBopProcessBom(this IRepository<BopProcessBom> repository, int bopBaseId)
        {
            return repository.Query().Where(p => p.BopBaseId == bopBaseId).ToList();
        }
    }
}
