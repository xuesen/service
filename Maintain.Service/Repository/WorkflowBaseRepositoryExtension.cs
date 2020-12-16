using System;
using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Core.Model;

namespace IIMes.Services.Maintain.Repository
{
    public static class WorkflowBaseRepositoryExtension
    {
        public static SWorkflowBase GetWorkflowByPartId(this IRepository<SWorkflowBase> repository, int partid)
        {
            return repository.Query().Where(p => p.SPart.Id == partid && p.LastVersion != null).FirstOrDefault();
        }

        public static SWorkflowBase GetWorkflowByFamilyId(this IRepository<SWorkflowBase> repository, int familyid)
        {
            return repository.Query().Where(p => p.SFamily.Id == familyid && p.LastVersion != null).FirstOrDefault();
        }
    }
}
