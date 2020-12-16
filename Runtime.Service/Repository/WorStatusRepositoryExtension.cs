using System.Collections.Generic;
using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Runtime.Interface.DTO;
using IIMes.Services.Runtime.Model.WOR;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace IIMes.Services.Runtime.Repository
{
    public static class WorStatusRepositoryExtension
    {
        public static WorStatus GetWorStatusByWo(this IRepository<WorStatus> repository, string wo)
        {
            return repository.Query().Where(p => p.WorkOrder == wo).ToList().FirstOrDefault();
        }

        public static WorStatus GetWorStatus(this IRepository<WorStatus> repository, string wo, int processid)
        {
            return repository.Query().Where(p => p.WorkOrder == wo && p.ProcessId == processid).ToList().FirstOrDefault();
        }
    }
}
