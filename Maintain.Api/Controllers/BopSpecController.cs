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
    public class BopSpecController : BaseController<SBopSpec, SBopSpecDTO>
    {
        public BopSpecController(ILogger<SBopSpec> logger, IBopSpecService service)
        : base(logger, service)
        {
        }

        [Route("getdata")]
        [HttpPost]
        public ActionResult Getdata()
        {
            // S_BARCODE_RULE
            return Ok(null);
        }
    }
}