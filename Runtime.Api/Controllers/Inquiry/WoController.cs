using System;
using System.Collections;
using System.Collections.Generic;
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
    public class WoController : BaseController
    {
        private readonly ILogger<WoController> _logger;
        private readonly IWoService _woService;

        public WoController(ILogger<WoController> logger, IWoService woService)
        {
            _logger = logger;
            _woService = woService;
        }

        // 检查输入工单是否存在
        [Route("{wo}/{statusname}")]
        [HttpGet]
        public ActionResult GetWo(string wo, string statusname)
        {
            var woObj = _woService.GetWo(wo, statusname);
            return Ok(woObj);
        }

        // 获取工单状态为WIP和RELEASE的工单列表
        [HttpPost]
        public ActionResult Query([FromBody]dynamic request)
        {
            WoRequestDTO woRequestdto = request.ToObject<WoRequestDTO>();
            var wolstObj = _woService.Query(woRequestdto);
            return Ok(wolstObj);
        }

        // [Route("barcode")]
        // [HttpPost]
        // public ActionResult Barcode([FromBody]dynamic request)
        // {
        //     var barcodelstObj = _woService.GenerateBarCode(14, 10, "100020200901-00", "NCAP-2D42-SR1");
        //     return Ok(barcodelstObj);
        // }
    }
}