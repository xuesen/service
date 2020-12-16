using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Runtime.Model.Process;
using NHibernate.Linq;

namespace IIMes.Services.Runtime.Repository
{
    public static class BopBaseRepositoryExtension
    {
        public static BopBase GetBopBaseByWorkflowId(this IRepository<BopBase> repository, int workflowId)
        {
            return repository.Query().Where(p => p.WorkflowId == workflowId && p.Status == "1").ToList().FirstOrDefault();
        }
    }
}
