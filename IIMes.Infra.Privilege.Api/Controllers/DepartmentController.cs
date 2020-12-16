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
    public class DepartmentController : BaseController<Department, DepartmentDTO>
    {
        private readonly IDepartmentService _departmentService;          
        public DepartmentController(ILogger<Department> logger, IDepartmentService departmentService)
        : base(logger, departmentService)
        {
            _departmentService = departmentService;
        }

        // 查询条件是上级部门的查询接口
        [Route("higherdep")]
        [HttpPost]
        public ActionResult GetDepartment([FromBody]DepartmentRequestDTO request)
        {
            var departmentObj = _departmentService.GetDepartment(request.DepartmentName);
            return Ok(departmentObj);
        }
    }
}