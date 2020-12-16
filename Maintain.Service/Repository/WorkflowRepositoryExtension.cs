using System;
using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Core.Model;

namespace IIMes.Services.Maintain.Repository
{
    public static class WorkflowRepositoryExtension
    {
        public static SWorkflow GetWorkflow(this IRepository<SWorkflow> repository, int workflowbaseid, string version)
        {
            return repository.Query().Where(p => p.SWorkflowBase.Id == workflowbaseid && p.Version == version).FirstOrDefault();
        }
    }
}
