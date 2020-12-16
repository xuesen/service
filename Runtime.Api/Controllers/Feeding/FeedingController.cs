using System;
using IIMes.Infrastructure.BaseController;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Query;
using IIMes.Services.Core.Model;
using IIMes.Services.Runtime.Interface;
using IIMes.Services.Runtime.Interface.DTO;
using IIMes.Services.Runtime.Model.Production;
using IIMes.Services.Runtime.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IIMes.Services.Runtime.Controllers
{
    public class FeedingController : BaseController
    {
        private readonly ILogger<FeedingController> _logger;
        private readonly IFeedingService _feedingService;

        public FeedingController(ILogger<FeedingController> logger, IFeedingService feedingService)
        {
            _logger = logger;
            _feedingService = feedingService;
        }

        // 质检确认上料
        [Route("confirmFeeding")]
        [HttpPut]
        public ActionResult ConfirmFeeding([FromBody] FeedingRequestDTO requestDTO)
        {
            var result = _feedingService.ConfirmFeeding(requestDTO);
            return Ok(BaseResult<object>.CreateOkResult(result));
        }

        // 查询FS_STATUS
        [Route("queryFsStatus")]
        [HttpGet]
        public ActionResult QueryFsStatus(string wo, int terminalId)
        {
            var result = _feedingService.QueryFsStatus(wo, terminalId);
            return Ok(BaseResult<object>.CreateOkResult(result));
        }

        // 更新FS_STATUS状态
        [Route("updateFsStatus")]
        [HttpPut]
        public ActionResult UpdateFsStatus([FromBody] FeedingRequestDTO requestDTO)
        {
            var result = _feedingService.UpdateFsStatus(requestDTO);
            return Ok(BaseResult<object>.CreateOkResult(result));
        }

        // 检查WoFs和TerminalPart
        [Route("check")]
        [HttpPut]
        public ActionResult Check([FromBody] FeedingRequestDTO requestDTO)
        {
            var reslut = _feedingService.Check(requestDTO);
            return Ok(BaseResult<object>.CreateOkResult(reslut));
        }

        // 下料显示界面(SMT)
        [Route("unfeedingForSMTShow")]
        [HttpPost]
        public ActionResult UnfeedingForSMTShow([FromBody] FeedingRequestDTO requestDTO)
        {
            var reslut = _feedingService.UnfeedingForSMTShow(requestDTO);
            return Ok(reslut);
        }

        // 下料显示界面(Bom)
        [Route("unfeedingForBomShow")]
        [HttpPost]
        public ActionResult UnfeedingForBomShow([FromBody] FeedingRequestDTO requestDTO)
        {
            var reslut = _feedingService.UnfeedingForBomShow(requestDTO);
            return Ok(reslut);
        }

        // 下料操作(SMT and Bom)
        [Route("unfeeding")]
        [HttpPost]
        public ActionResult Unfeeding([FromBody] TerminalPartsBySMT requestDTO)
        {
            _feedingService.Unfeeding(requestDTO);
            return Ok(null);
        }

        // 检查WoBOM和TerminalPart
        [Route("checkWoBom")]
        [HttpPost]
        public ActionResult CheckWoBom([FromBody] FeedingRequestDTO requestDTO)
        {
            var reslut = _feedingService.CheckWoBom(requestDTO);
            return Ok(reslut);
        }

    }
}