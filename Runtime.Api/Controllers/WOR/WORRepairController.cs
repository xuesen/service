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
    public class WORRepairController : BaseController
    {
        private readonly ILogger<WORRepairController> _logger;
        private readonly IWORRepairService _worrepairService;

        public WORRepairController(ILogger<WORRepairController> logger, IWORRepairService worrepairService)
        {
            _logger = logger;
            _worrepairService = worrepairService;
        }

        // 刷入SN时调用，带出已检测的不良信息
        [Route("sn")]
        [HttpPost]
        public ActionResult InputSN(WorRepairRequestDTO request)
        {
            var result = _worrepairService.InputSN(request);
            return Ok(BaseResult<object>.CreateOkResult(result));
        }

        // 维修不良时调用
        [Route("repair")]
        [HttpPost]
        public ActionResult SaveRepair(WorRepairItemInfoDTO request)
        {
            var result = _worrepairService.SaveRepair(request);
            return Ok(BaseResult<object>.CreateOkResult(result));
        }

        // 点击完成，保存过站
        [Route("finish")]
        [HttpPost]
        public IActionResult FinishRepair(WorRepairFinishRequestDTO request)
        {
            var result = _worrepairService.FinishRepair(request);
            return Ok(BaseResult<object>.CreateOkResult(result));
        }

        // 保存新增不良现象时调用
        [Route("defect")]
        [HttpPost]
        public IActionResult SaveDefect(WorDefectRequestDTO request)
        {
            var result = _worrepairService.SaveDefect(request);
            return Ok(BaseResult<object>.CreateOkResult(result));
        }

        // 换料时获取不良物料序号时调用
        [Route("defectkeyparts")]
        [HttpPost]
        public IActionResult GetDefectKeyParts(WorDefectKeypartsRequestDTO request)
        {
            var result = _worrepairService.GetDefectKeyParts(request);
            return Ok(BaseResult<object>.CreateOkResult(result));
        }

        // 换料时保存时调用
        [HttpPost]
        [Route("keyparts")]
        public IActionResult SaveNewKeyParts(WorDefectKeypartsSaveRequestDTO request)
        {
            _worrepairService.SaveNewKeyParts(request);
            return Ok(BaseResult<string>.CreateOkResult("ok"));
        }  

        // 获取维修历史
        [HttpGet]        
        [Route("repairhistory/{sn}")]
        public IActionResult GetRepairHistory([FromRoute]string sn)
        {
            var result = _worrepairService.GetRepairHistory(sn);
            return Ok(BaseResult<object>.CreateOkResult(result));
        }

        // 获取换料历史
        [Route("keypartshistory/{sn}")]
        [HttpGet]
        public IActionResult GetKeyPartsHistory([FromRoute]string sn)
        {
            var result = _worrepairService.GetKeyPartsHistory(sn);
            return Ok(BaseResult<object>.CreateOkResult(result));            
        }

        // 删除不良现象时调用
        [HttpDelete]
        [Route("defect/{id}")]
        public IActionResult DeleteDefect([FromRoute]int id)
        {
            _worrepairService.DeleteDefect(id);
            return Ok(BaseResult<string>.CreateOkResult("ok"));
        }

        // 删除维修信息时调用
        [HttpDelete]
        [Route("repairinfo/{id}")]
        public IActionResult DeleteRepairInfo([FromRoute]int id)
        {
            _worrepairService.DeleteRepairInfo(id);
            return Ok(BaseResult<string>.CreateOkResult("ok"));
        }
    }
}