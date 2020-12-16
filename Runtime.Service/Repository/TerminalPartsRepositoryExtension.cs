using System.Collections.Generic;
using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Runtime.Model.Production;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace IIMes.Services.Runtime.Repository
{
    public static class TerminalPartsRepositoryExtension
    {
        public static IList<TerminalParts> GetTerminalPartsByTerminalId(this IRepository<TerminalParts> repository, int terminalId)
        {
            return repository.Query().Where(o => o.TerminalId == terminalId).OrderBy(o => o.Sequence).ToList();
        }
        public static void DeleteByIds(this IRepository<TerminalParts> repository, ICollection<int> Ids)
        {
            repository.DeleteByLambda(o => Ids.Contains(o.Id));
        }
        public static void DeleteById(this IRepository<TerminalParts> repository, int id)
        {
            repository.DeleteByLambda(o => o.Id == id);
        }

        public static void DeleteByScanPartId(this IRepository<TerminalParts> repository, int scanpartid)
        {
            repository.DeleteByLambda(o => o.ScanPartId == scanpartid);
        }

        public static IList<TerminalParts> GetTerminalPartsByStation(this IRepository<TerminalParts> repository, string station)
        {
            return repository.Query().Where(o => o.Station == station).ToList();
        }
    }
}
