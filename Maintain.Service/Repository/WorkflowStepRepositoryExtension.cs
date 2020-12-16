using System;
using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Core.Model;

namespace IIMes.Services.Maintain.Repository
{
    public static class WorkflowStepRepositoryExtension
    {
        public static IQueryable<SWorkflowStep> GetWorkFlowStep(this IRepository<SWorkflowStep> repository, int workflowid)
        {
            return repository.Query().Where(p => p.SWorkflow.Id == workflowid);
        }
    }
}
