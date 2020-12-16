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
    public static class WorkflowRepositoryExtension
    {
        public static Workflow GetWorkflow(this IRepository<Workflow> repository, int id)
        {
            return repository.Query().Where(p => p.Id == id).FirstOrDefault();
        }
    }
}
