using System;
using IIMes.Infrastructure.BaseController;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Query;
using IIMes.Infra.Privilege.Interface;
using IIMes.Infra.Privilege.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IIMes.Infra.Privilege.Controllers
{
    public class CompanyController : BaseController<Company, CompanyDTO>
    {
        public CompanyController(ILogger<Company> logger, ICompanyService companyService)
        : base(logger, companyService)
        {
        }
    }
}