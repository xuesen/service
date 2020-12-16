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
    public class RoleController : BaseController<Role, RoleDTO>
    {
        public RoleController(ILogger<Role> logger, IRoleService roleService)
        : base(logger, roleService)
        {
        }
    }
}