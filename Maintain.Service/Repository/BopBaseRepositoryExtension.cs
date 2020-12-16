using System;
using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Core.Model;

namespace IIMes.Services.Maintain.Repository
{
    public static class BopBaseRepositoryExtension
    {
        public static SBopBase GetBopBase(this IRepository<SBopBase> repository, int workflowid)
        {
            return repository.Query().Where(p => p.SWorkflow.Id == workflowid && p.Status == "1").FirstOrDefault();
        }
    }
}
