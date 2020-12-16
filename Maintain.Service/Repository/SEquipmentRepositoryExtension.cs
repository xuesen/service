using System;
using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Core.Model;

namespace IIMes.Services.Maintain.Repository
{
    public static class SEquipmentRepositoryExtension
    {
        public static SEquipment GetEquipment(this IRepository<SEquipment> repository, string equipmentcode)
        {
            return repository.Query().Where(p => p.Code == equipmentcode).FirstOrDefault();
        }
    }
}
