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
    public class RoleResourcePermissionController : BaseController
    {
        private readonly ILogger<RoleResourcePermissionController> _logger;
        private readonly IRoleResourcePermissionService _roleresourcepermissionService;
        public RoleResourcePermissionController(ILogger<RoleResourcePermissionController> logger, IRoleResourcePermissionService roleresourcepermissionService)
        {
            _logger = logger;
            _roleresourcepermissionService = roleresourcepermissionService;
        }

        [Route("rrp")]
        [HttpPost]
        public ActionResult Save([FromBody]RoleSourcePermissionRequestDTO request)
        {
            _roleresourcepermissionService.Save(request);
            return Ok(BaseResult<string>.CreateOkResult("ok"));
        }        
    }
}