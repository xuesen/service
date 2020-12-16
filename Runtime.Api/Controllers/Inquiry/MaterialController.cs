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
    public class MaterialController : BaseController
    {
        private readonly ILogger<MaterialController> _logger;
        private readonly IMaterialService _materialService;

        public MaterialController(ILogger<MaterialController> logger, IMaterialService materialService)
        {
            _logger = logger;
            _materialService = materialService;
        }

        // <summary>
        // 刷入的REELID对应物料代码/PN（material表获取REELID与PN对应关系）
        // </summary>
        // <param name="request">{'inputno':}</param>
        // <returns></returns>
        [Route("reel/{inputno}")]
        [HttpGet]
        public ActionResult GetPartByReel([FromRoute]string inputno)
        {
            var partObj = _materialService.GetPartByReel(inputno);
            return Ok(partObj);
        }

        // <summary>
        // 刷入的Part对应物料代码/PN（material表获取REELID与PN对应关系）
        // </summary>
        // <param name="request">{'inputno':}</param>
        // <returns></returns>
        [Route("part/{inputno}")]
        [HttpGet]
        public ActionResult GetPartByPart([FromRoute]string inputno)
        {
            var partObj = _materialService.GetPartByPart(inputno);
            return Ok(partObj);
        }
    }
}