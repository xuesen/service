using System;
using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Core.Model;

namespace IIMes.Services.Maintain.Repository
{
    public static class BarcodeRuleRepositoryExtension
    {
        public static IQueryable<SBarcodeRule> GetBarcodeRule(this IRepository<SBarcodeRule> repository, int ruleid)
        {
            return repository.Query().Where(p => p.SSetting.Id == ruleid).OrderBy(p => p.RuleName);
        }
    }
}
