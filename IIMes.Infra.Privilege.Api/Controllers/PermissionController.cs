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
    public class PermissionController : BaseController<Permission, PermissionDTO>
    {
        private readonly IPermissionService _permissionService;        
        public PermissionController(ILogger<Permission> logger, IPermissionService permissionService)
        : base(logger, permissionService)
        {
            _permissionService = permissionService;
        }

        // 获取权限树
        [Route("permissiontree/{application}")]
        [HttpGet]
        public ActionResult GetPermissionTree(string application)
        {
            var result = _permissionService.GetPermissionTree(application);
            return Ok(result);
        }

        // 获取当前角色的权限树
        [Route("permissiontree/{application}/{roleid}")]
        [HttpGet]
        public ActionResult GetPermissionTreeByRole(string application, int roleid)
        {
            var result = _permissionService.GetPermissionTreeByRole(application, roleid);
            return Ok(result);
        }

    }
}