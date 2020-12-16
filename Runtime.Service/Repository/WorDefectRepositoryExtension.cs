using System.Collections.Generic;
using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Runtime.Model.Process;
using IIMes.Services.Runtime.Model.Product;
using IIMes.Services.Runtime.Model.Production;
using IIMes.Services.Runtime.Model.WOR;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace IIMes.Services.Runtime.Repository
{
    public static class WorDefectRepositoryExtension
    {
        public static IQueryable<WorDefect> GetWorDefects(this IRepository<WorDefect> repository, string sn)
        {
            return repository.Query().Where(p => p.SerialNumber == sn);
        } 

        public static IQueryable<WorDefect> GetWorDefectsByLogId(this IRepository<WorDefect> repository, int logid)
        {
            return repository.Query().Where(p => p.TestWorLogId == logid);
        }     
        public static IQueryable<WorDefect> GetWorDefectsByDefectCode(this IRepository<WorDefect> repository, string sn, string defectcode, int logid)
        {
            return repository.Query().Where(p => p.SerialNumber == sn && p.DefectCode == defectcode && p.TestWorLogId == logid);
        }  
    }
}
