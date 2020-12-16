using IIMes.Infrastructure.BaseController;
using IIMes.Services.Maintain.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IIMes.Services.Maintain.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaintainController : BaseController
    {
        private readonly ILogger<MaintainController> _logger;
        private readonly IMaintainService _maintainService;

        public MaintainController(ILogger<MaintainController> logger,  IMaintainService maintainService)
        {
            _logger = logger;
            _maintainService = maintainService;
        }

        // 获取通用下拉选项内容
        [Route("/api/maintain/{tablename}")]
        [HttpGet]
        public ActionResult Get(string tablename)
        {
            var commonObj = _maintainService.GetCommonInfo(tablename);
            return Ok(commonObj);
        }

        // 根据线别获取设备信息
        [Route("/api/maintain/equipment/{lineid}")]
        [HttpGet]
        public ActionResult GetEquipmentbyLine(int lineid)
        {
            var commonObj = _maintainService.GetEquipmentbyLine(lineid);
            return Ok(commonObj);
        }

        // 获取产品物料
        [Route("/api/maintain/parts")]
        [HttpGet]
        public ActionResult GetParts()
        {
            var partObj = _maintainService.GetParts();
            return Ok(partObj);
        }

        // 获取自产物料
        [Route("/api/maintain/marketparts")]
        [HttpGet]
        public ActionResult GetMarketParts()
        {
            var partObj = _maintainService.GetMarketParts();
            return Ok(partObj);
        }

        // 获取采购物料
        [Route("/api/maintain/catchparts")]
        [HttpGet]
        public ActionResult GetCatchParts()
        {
            var partObj = _maintainService.GetCatchParts();
            return Ok(partObj);
        }

        // 从S_SETTING表获取数据
        [Route("/api/maintain/setting/{program}")]
        [HttpGet]
        public ActionResult GetSetting([FromRoute]string program)
        {
            // Side: program='Side'
            var commonObj = _maintainService.GetSetting(program);
            return Ok(commonObj);
        }
    }
}
