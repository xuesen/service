using System;
using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Core.Model;

namespace IIMes.Services.Maintain.Repository
{
    public static class SProcessRepositoryExtension
    {
        public static SProcess GetProcess(this IRepository<SProcess> repository, string processCode)
        {
            return repository.Query().Where(p => p.Code == processCode).FirstOrDefault();
        }
    }
}
