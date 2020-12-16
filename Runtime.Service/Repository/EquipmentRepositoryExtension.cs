using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Runtime.Model.Resource;

namespace IIMes.Services.Runtime.Repository
{
    public static class EquipmentRepositoryExtension
    {
        public static Equipment GetEquipment(this IRepository<Equipment> repository, string equipmentcode)
        {
            return repository.Query().Where(p => p.Code == equipmentcode).FirstOrDefault();
        }
    }
}
