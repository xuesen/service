using System;
using System.Linq;
using IIMes.Infrastructure.BaseController;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Query;
using IIMes.Services.Core.Model;
using IIMes.Services.Maintain.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IIMes.Services.Maintain.Controllers
{
    public class PartPackController : BaseController<SPartPack, SPartPackDTO>
    {
        public PartPackController(ILogger<SPartPack> logger, IPartPackService service)
        : base(logger, service)
        {
        }

        // 测试
        [Route("test")]
        [HttpPost]
        public ActionResult Test()
        {
            return Ok(null);
        }
    }
}