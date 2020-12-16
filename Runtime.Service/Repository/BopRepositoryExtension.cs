using System.Collections.Generic;
using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Runtime.Model.Process;
using IIMes.Services.Runtime.Model.Product;
using IIMes.Services.Runtime.Model.Production;
using NHibernate.Linq;

namespace IIMes.Services.Runtime.Repository
{
    public static class BopRepositoryExtension
    {
        public static Bop GetBopByWo(this BopRepository repository, string wo)
        {
            var bop = repository.GetBop(wo);
            return bop;
        }

        public static Bop GetBopByWoAndEquipment(this BopRepository repository, string wo, int equipmentId)
        {
            var bop = repository.GetBopByWoAndEquipment(wo, equipmentId);
            return bop;
        }
    }
}
