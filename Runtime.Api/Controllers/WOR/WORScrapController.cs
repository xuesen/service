using System;
using IIMes.Infrastructure.BaseController;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Query;
using IIMes.Services.Core.Model;
using IIMes.Services.Runtime.Interface;
using IIMes.Services.Runtime.Interface.DTO;
using IIMes.Services.Runtime.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IIMes.Services.Runtime.Controllers
{
    public class WORScrapController : BaseController
    {
        private readonly ILogger<WORScrapController> _logger;
        private readonly IWORScrapService _worscrapService;

        public WORScrapController(ILogger<WORScrapController> logger, IWORScrapService worscrapService)
        {
            _logger = logger;
            _worscrapService = worscrapService;
        }

        // 报废, 获取报废原因
        [Route("scrapreason")]
        [HttpGet]
        public ActionResult QueryScrapReason()
        {
            var result = _worscrapService.QueryScrapReason();
            return Ok(BaseResult<object>.CreateOkResult(result));
        }

        // 保存报废信息
        // <returns></returns>
        [Route("wor")]
        [HttpPost]
        public ActionResult SaveWorScrap([FromBody]dynamic request)
        {
            dynamic worscrapRequestdto = request.ToObject<WorScrapRequestDTO>();
            var ret = _worscrapService.SaveWorScrap(worscrapRequestdto);
            return Ok(BaseResult<object>.CreateOkResult(ret));
        }
    }
}