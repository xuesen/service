using System;
using IIMes.Infrastructure.BaseController;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Query;
using IIMes.Services.Core.Model;
using IIMes.Services.Maintain.Interface;
using IIMes.Services.Maintain.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IIMes.Services.Maintain.Controllers
{
    public class EmployeeController : BaseController<SEmployee, EmployeeDTO>
    {
        public EmployeeController(ILogger<SEmployee> logger, IEmployeeService employeeService)
        : base(logger, employeeService)
        {
        }
    }
}