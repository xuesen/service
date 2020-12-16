// using System.Net.Cache;
// using System;
// using System.Threading;
// using System.Threading.Tasks;
// using IIMes.Services.Core.Model;
// using IIMes.Services.Maintain.Model;
// using MassTransit;
// using MassTransit.Util;
// using IIMes.Services.Core.Message;
using AutoMapper;
using IIMes.Infrastructure.Aspect;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Exception;
using IIMes.Infrastructure.Service;
using IIMes.Infra.Privilege.Model;
using IIMes.Infra.Privilege.Interface;

namespace IIMes.Infra.Privilege.Services
{
    public partial class CompanyService : BaseMaintainService<Company, CompanyDTO>, ICompanyService
    {
        public CompanyService(IRepository<Company> rep, IMapper mapper)
        : base(rep, mapper)
        {
        }
    }
}
