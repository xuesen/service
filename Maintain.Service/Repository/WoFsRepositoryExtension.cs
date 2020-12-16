using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Core.Model;

namespace IIMes.Services.Maintain.Repository
{
    public static class WoFsRepositoryExtension
    {
        public static WoFs GetWoFs(this IRepository<WoFs> repository, string workOrder)
        {
            return repository.Query().Where(p => p.WorkOrder == workOrder).FirstOrDefault();
        }

        // public static IQueryable<WoFs> GetWoFslist(this IRepository<WoFs> repository, string workOrder, string pdlineid, string equipmentid)
        // {
        //     if (pdlineid != "" && equipmentid != "")
        //     {
        //         return repository.Query().Where(p => p.WorkOrder == workOrder && p.EquipmentId == int.Parse(equipmentid));
        //     }
        //     else if (pdlineid != "" && equipmentid == "")
        //     {
        //         return repository.Query().Where(p => p.WorkOrder == workOrder && p.EquipmentId == int.Parse(equipmentid));
        //     }
        // }

        public static WoFs CheckWoFsData(this IRepository<WoFs> repository, string workOrder, int pdLineId, int equipmentId, string side)
        {
            return repository.Query().Where(p => p.WorkOrder == workOrder && p.EquipmentId == equipmentId && p.Side == side).FirstOrDefault();
        }

        public static WoFs GetWoFsByWoFs(this IRepository<WoFs> repository, WoFs wofs)
        {
            return repository.Query().Where(
                p => p.WorkOrder == wofs.WorkOrder && p.PartId == wofs.PartId && p.EquipmentId == wofs.EquipmentId && p.Side == wofs.Side).FirstOrDefault();
        }
    }
}
