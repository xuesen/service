using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Runtime.Model.WOR;

namespace IIMes.Services.Runtime.Repository
{
    public static class WorRepairLogRepositoryExtension
    {
        public static WorRepairLog GetRepairLog(this IRepository<WorRepairLog> repository, string sn)
        {
            return repository.Query().Where(p => p.SerialNumber == sn).FirstOrDefault();
        }
        public static WorRepairLog GetRepairLogBySn(this IRepository<WorRepairLog> repository, string sn)
        {
            return repository.Query().Where(p => p.SerialNumber == sn && p.RepairWorLogId == null).FirstOrDefault();
        }
        public static WorRepairLog GetRepairLogByTestWorLogId(this IRepository<WorRepairLog> repository, string sn, int testworlogid)
        {
            return repository.Query().Where(p => p.SerialNumber == sn && p.TestWorLogId == testworlogid).OrderBy(p => p.Cdt).FirstOrDefault();
        }
        public static WorRepairLog GetRepairLogByProcess(this IRepository<WorRepairLog> repository, string sn, string processcode)
        {
            return repository.Query().Where(p => p.SerialNumber == sn && p.ProcessCode == processcode).OrderByDescending(p => p.Cdt).FirstOrDefault();
        }

        public static IQueryable<WorRepairLog> GetRepairLogByUdt(this IRepository<WorRepairLog> repository, string sn)
        {
            return repository.Query().Where(p => p.SerialNumber == sn && p.UpdateTime != null);
        }
    }
}
