using System;
using System.Collections.Generic;
using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Runtime.Model.Process;
using IIMes.Services.Runtime.Model.Product;
using IIMes.Services.Runtime.Model.Production;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace IIMes.Services.Runtime.Repository
{
    public static class FsStatusRepositoryExtension
    {
        public static FsStatus GetFsStatus(this IRepository<FsStatus> repository, string wo, int terminalid)
        {
            return repository.Query().Where(p => p.WorkOrder == wo && p.TerminalId == terminalid).FirstOrDefault();
        }

        public static FsStatus UpdateFsStatus(this IRepository<FsStatus> repository, string wo, int terminalid, int status)
        {
            var fsStatus = repository.Query().Where(p => p.WorkOrder == wo && p.TerminalId == terminalid).FirstOrDefault();
            fsStatus.Status = status;
            fsStatus.Udt = DateTime.Now;
            return repository.Update(fsStatus);
        }

        public static FsStatus UpdateFsStatusList(this IRepository<FsStatus> repository, int equipmentId, int terminalid, int status)
        {
            var collection = repository.Query().Where(p => p.EquipmentId == equipmentId && p.TerminalId == terminalid);
            foreach (var fsStatus in collection)
            {
                fsStatus.Status = status;
                fsStatus.Udt = DateTime.Now;
                repository.Update(fsStatus);
            }

            return null;
        }
    }
}
