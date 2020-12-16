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
    public class BopProcessBomController : BaseController<SBopProcessBom, SBopProcessBomDTO>
    {
        private readonly IBopProcessBomService _service;

        public BopProcessBomController(ILogger<SBopProcessBom> logger, IBopProcessBomService service)
        : base(logger, service)
        {
            _service = service;
        }

        // 工序下物料清单
        [Route("search")]
        [HttpPost]
        public ActionResult Search([FromBody] dynamic request)
        {
            var requestDto = request.ToObject<BopProcessRequestDTO>();
            var ret = _service.Search(requestDto);
            return Ok(ret);
        }
    }
}