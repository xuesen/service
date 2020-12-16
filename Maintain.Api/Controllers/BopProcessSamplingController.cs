using System;
using IIMes.Infrastructure.BaseController;
using IIMes.Infrastructure.Query;
using IIMes.Services.Core.Model;
using IIMes.Services.Maintain.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IIMes.Services.Maintain.Controllers
{
    public class BopProcessSamplingController : BaseController<SBopProcessSampling, SBopProcessSamplingDTO>
    {
        private IBopProcessSamplingService _service;

        public BopProcessSamplingController(ILogger<SBopProcessSampling> logger, IBopProcessSamplingService service)
        : base(logger, service)
        {
            _service = service;
        }

        // 工序下抽检计划
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