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
    public class WORController : BaseController
    {
        private readonly ILogger<WORController> _logger;
        private readonly IWORService _worService;

        public WORController(ILogger<WORController> logger, IWORService worService)
        {
            _logger = logger;
            _worService = worService;
        }

        // 开工，更新工单状态
        [Route("wos/{wo}")]
        [HttpPost]
        public ActionResult SaveWos([FromRoute]string wo)
        {
            _worService.SaveWos(wo);
            return Ok(BaseResult<string>.CreateOkResult("ok"));
        }

        // 报工，进站检查
        // 包括： SMT IN（SMT首站）需做检查
        // <returns></returns>
        [Route("firstprocess")]
        [HttpPost]
        public ActionResult CheckSMTFirstProcess(WorRequestDTO wor)
        {
            var ret =  _worService.CheckSMTFirstProcess(wor);
            // ret = true;表示是SMT首站
            return Ok(ret);
        }

        // 报工，进站检查
        // 检查当前站无Fail分支时，输入不良品数量框灰掉处理
        // <returns></returns>
        [Route("workflowstep")]
        [HttpPost]
        public ActionResult CheckWorkflowStep(WorRequestDTO wor)
        {
            var ret =  _worService.CheckWorkflowStep(wor);
            // ret = false 表示无fail分支，输入不良品数量框灰掉处理
            // var ret = true;
            return Ok(ret);
        }

        // 获取WorStatus数据
        // <returns></returns>
        [Route("worstatus")]
        [HttpPost]
        public ActionResult GetWorStatus([FromBody]WorRequestDTO request)
        {
            var worObj = _worService.GetWorStatus(request);
            return Ok(worObj);
        }

        // 报工：保存报工信息
        // 质检报工
        // <returns></returns>
        [Route("wor")]
        [HttpPost]
        public ActionResult SaveWor([FromBody]WorRequestDTO request)
        {
            _worService.SaveWor(request);
            return Ok(BaseResult<string>.CreateOkResult("ok"));
        }

        // 报工，进站检查
        // 包括： 是否为最后一个工序，需做检查
        // <returns></returns>
        [Route("lastprocess")]
        [HttpPost]
        public ActionResult CheckLastProcess(WorRequestDTO wor)
        {
            var ret =  _worService.CheckLastProcess(wor);
            // ret = true;表示是最后一个工序
            return Ok(ret);
        }

        // 报工，质检不良报工时刷入不良SN时需做该检查
        // request.SN , 刷入序号在WOR_DEFECT存在记录，报错
        // <returns></returns>
        [Route("wor/{sn}")]
        [HttpGet]
        public ActionResult CheckWorRepair([FromRoute]string sn)
        {
            // modify ITC-1755-0181
             _worService.CheckWorRepair(sn);
            return Ok();
        }
    }
}