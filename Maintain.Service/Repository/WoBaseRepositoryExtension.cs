using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Core.Model;
using NHibernate.Linq;

namespace IIMes.Services.Maintain.Repository
{
    public static class WoBaseRepositoryExtension
    {
        public static WoBase GetWo(this IRepository<WoBase> repository, string workOrder)
        {
            return repository.Query().Where(p => p.WorkOrder == workOrder).FirstOrDefault();
        }

        public static object CheckPartWo(this IRepository<WoBase> repository, int partid, int statusid)
        {
            return repository.Query().Where(p => p.SPart.Id == partid && p.StatusId < statusid).Select(p => p.WorkOrder);
        }

        public static IQueryable<WoBase> QueryWos(this IRepository<WoBase> repository, string wo, string wostatus, string partno, string woscheduletime, string pdline)
        {
            return repository.Query().Where(p => p.WorkOrder.Like("%" + wo + "%") && p.SWoStatus.Code.Like("%" + wostatus + "%") && p.SPart.PartNo.Like("%" + partno + "%")
             && p.SProductLine.Code.Like("%" + pdline + "%") && p.WoScheduleTime.ToString().Like("%" + woscheduletime + "%"));
        }
    }
}
