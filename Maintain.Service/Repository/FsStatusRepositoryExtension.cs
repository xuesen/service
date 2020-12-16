using System;
using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Core.Model;

namespace IIMes.Services.Maintain.Repository
{
    public static class FsStatusRepositoryExtension
    {
        public static FsStatus GetWoFs(this IRepository<FsStatus> repository, string workOrder)
        {
            return repository.Query().Where(p => p.WorkOrder == workOrder).FirstOrDefault();
        }
    }
}
