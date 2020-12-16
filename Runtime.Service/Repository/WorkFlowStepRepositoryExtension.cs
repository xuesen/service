using System.Collections.Generic;
using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Runtime.Model.Process;
using IIMes.Services.Runtime.Model.Product;
using IIMes.Services.Runtime.Model.Production;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace IIMes.Services.Runtime.Repository
{
    public static class WorkFlowStepRepositoryExtension
    {
        public static WorkflowStep GetWorkFlowStepByNextProcessId(this IRepository<WorkflowStep> repository, int workflowid, int processid)
        {
            return repository.Query().Where(p => p.Workflow.Id == workflowid && p.NextProcess.Id == processid).FirstOrDefault();
        }

        public static WorkflowStep GetWorkFlowStepByProcessId(this IRepository<WorkflowStep> repository, int workflowid, int processid)
        {
            return repository.Query().Where(p => p.Workflow.Id == workflowid && p.Process.Id == processid).FirstOrDefault();
        }

        public static WorkflowStep GetWorkFlowStepByProcessIdAndCondition(this IRepository<WorkflowStep> repository, int workflowid, int processid, int conditionid)
        {
            return repository.Query().Where(p => p.Workflow.Id == workflowid && p.Process.Id == processid && p.Setting.Id == conditionid).FirstOrDefault();
        }

        public static IQueryable<WorkflowStep> GetWorkFlowStep(this IRepository<WorkflowStep> repository, int workflowid)
        {
            return repository.Query().Where(p => p.Workflow.Id == workflowid);
        }
    }
}
