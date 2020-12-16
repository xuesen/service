using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Core.Model;
using IIMes.Services.Maintain.Interface;

namespace IIMes.Services.Maintain.Repository
{
    public static class SPartFsRepositoryExtension
    {
        public static SPartFs GetPartFs(this IRepository<SPartFs> repository, PartFsDTO partFsDTO)
        {
            return repository.Query().Where(p => p.PartId == partFsDTO.PartId && p.EquipmentId == partFsDTO.EquipmentId && p.Side == partFsDTO.Side).OrderByDescending(p => p.CreateTime).FirstOrDefault();
        }
    }
}
