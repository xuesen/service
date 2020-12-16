using System;
using IIMes.Infrastructure.BaseController;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Query;
using IIMes.Infra.Privilege.Model;
using IIMes.Infra.Privilege.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IIMes.Infra.Privilege.Controllers
{
    public class RoleEmployeeController : BaseController<RoleEmployee, RoleEmployeeDTO>
    {
        public RoleEmployeeController(ILogger<RoleEmployee> logger, IRoleEmployeeService roleemployeeService)
        : base(logger, roleemployeeService)
        {
        }
    }
}