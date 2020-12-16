using System;
using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Core.Model;

namespace IIMes.Services.Maintain.Repository
{
    public static class BopRepositoryExtension
    {
        public static SBop GetBop(this IRepository<SBop> repository, int workflowid)
        {
            return repository.Query().Where(p => p.WorkflowId == workflowid).FirstOrDefault();
        }
    }
}
