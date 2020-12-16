using System;
using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Core.Model;

namespace IIMes.Services.Maintain.Repository
{
    public static class SPartRepositoryExtension
    {
        public static SPart GetPart(this IRepository<SPart> repository, string partNo)
        {
            return repository.Query().Where(p => p.PartNo == partNo).FirstOrDefault();
        }
    }
}
