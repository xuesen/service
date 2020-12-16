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
    public class WORSymptomController : BaseController
    {
        private readonly ILogger<WORSymptomController> _logger;
        private readonly IWORSymptomService _worsymptomService;

        public WORSymptomController(ILogger<WORSymptomController> logger, IWORSymptomService worsymptomServic)
        {
            _logger = logger;
            _worsymptomService = worsymptomServic;
        }

        // 质检, 获取不良现象
        [Route("symptom")]
        [HttpGet]
        public ActionResult QuerySymptom()
        {
            var result = _worsymptomService.QuerySymptom();
            return Ok(BaseResult<object>.CreateOkResult(result));
        }
    }
}