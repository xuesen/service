using IIMes.Infrastructure.BaseController;
using IIMes.Services.Maintain.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IIMes.Services.Maintain.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WoFsController : BaseController
    {
        private const string Value = "ret.ToString()";
        private readonly ILogger<WoFsController> _logger;
        private readonly IWoFsService _wofsService;

        // 工单料站表维护
        public WoFsController(ILogger<WoFsController> logger,  IWoFsService wofsService)
        {
            _logger = logger;
            _wofsService = wofsService;
        }

        // <summary>
        // 查询工单料站表信息
        // </summary>
        // <param name="request">{"WorkOrder":"", "LineId":"", "SideCode": ""}</param>
        // <returns></returns>
        [Route("/api/wofs/wofsinfo")]
        [HttpPost]
        public ActionResult GetWoFsInfo([FromBody]dynamic request)
        {
            WoFsRequestDTO wofsrequestdto = request.ToObject<WoFsRequestDTO>();
            var wofsinfoObj = _wofsService.GetWoFsInfo(wofsrequestdto);
            return Ok(wofsinfoObj);
        }

        // 获取工单料站详细信息
        [Route("/api/wofs/wofsdetail/{wofsid}")]
        [HttpGet]
        public ActionResult GetWoFsDetail([FromRoute]int wofsid)
        {
            var wofsdetailObj = _wofsService.GetWoFsDetailInfo(wofsid);
            return Ok(wofsdetailObj);
        }

        // 已上料工单不能编辑、删除和加载：WO_FS.status不等于“未上料”
        [Route("/api/wofs/wostatus/{wofsid}")]
        [HttpGet]
        public ActionResult CheckWoStatus([FromRoute]int wofsid)
        {
            var ret = _wofsService.CheckWoStatus(wofsid);
            // ret = false 不允许加载、编辑和删除
            return Ok(ret);
        }

        // 加载机型料站表到工单料站表之前检查该工单是否已经加载过，如加载过提示是否覆盖
        [Route("/api/wofs/wofsdata")]
        [HttpPost]
        public ActionResult CheckWoFsData([FromBody]dynamic request)
        {
            ImportWoFsRequestDTO requestdto = request.ToObject<ImportWoFsRequestDTO>();
            var ret = _wofsService.CheckWoFsData(requestdto);
            // ret = true 提示是否覆盖
            return Ok(ret);
        }

        // 加载机型料站表到工单料站表
        [Route("/api/wofs/importpartfs2wofs")]
        [HttpPost]
        public ActionResult ImportPartFs2WoFs([FromBody]dynamic request)
        {
            ImportWoFsRequestDTO requestdto = request.ToObject<ImportWoFsRequestDTO>();
            _wofsService.ImportPartFs2WoFs(requestdto);
            return Ok(BaseResult<string>.CreateOkResult("ok"));
        }

        // 新增工单料站详细信息
        [Route("/api/wofs/wofsdetailinfo")]
        [HttpPost]
        public ActionResult SaveWoFsDetail([FromBody]dynamic request)
        {
            WoFsDetailRequestDTO wofsdetailrequest = request.ToObject<WoFsDetailRequestDTO>();
            _wofsService.SaveWoFsDetailInfo(wofsdetailrequest, 0);
            return Ok(BaseResult<string>.CreateOkResult("ok"));
        }

        // 编辑工单料站详细信息
        [Route("/api/wofs/wofsdetailinfo/{id}")]
        [HttpPut]
        public ActionResult UpdateWoFsDetail(int id, [FromBody]dynamic request)
        {
            WoFsDetailRequestDTO wofsdetailrequest = request.ToObject<WoFsDetailRequestDTO>();
            wofsdetailrequest.Id = id;
            _wofsService.SaveWoFsDetailInfo(wofsdetailrequest, 1);
            return Ok(BaseResult<string>.CreateOkResult("ok"));
        }

        // 删除工单料站详细信息
        [Route("/api/wofs/wofsdetail/{id}")]
        [HttpDelete]
        public ActionResult DeleteWoFsDetail([FromRoute]int id)
        {
            _wofsService.DeleteWoFsDetail(id);
            return Ok(BaseResult<string>.CreateOkResult("ok"));
        }

        // 从WO_BOM获取新增工单料站详细信息的物料，如果有替代料带出替代料
        // 界面从S_PART表中获取物料和替代料，不从WO_BOM获取，该功能去掉。 2020.7.23
/*      [Route("/api/wofs/wobompart/{wo}")]
        [HttpGet]
        public ActionResult GetWoBomPart([FromRoute]string wo)
        {
            var wobompartObj = _wofsService.GetWoBomPart(wo);
            return Ok(wobompartObj);
        } */

        // 新增工单料站替代料
        // 功能取消，合并到新增工单详细信息中。 2020.7.3
/*      [Route("/api/wofs/savewofssub")]
        [HttpPost]
        public ActionResult SaveWoFsSub([FromBody]dynamic request)
        {
            //_wofsService.SaveWoFsSub(request);
            return Ok(BaseResult<string>.CreateOkResult("ok"));
        } */
        // 删除工单料站替代料
        // 功能取消，合并到新增工单详细信息中。 2020.7.3
/*      [Route("/api/wofs/deletewofssub")]
        [HttpPost]
        public ActionResult DeleteWoFsSub([FromBody]dynamic request)
        {
            //_wofsService.DeleteWoFsSub(request);
            return Ok(BaseResult<string>.CreateOkResult("ok"));
        } */
    }
}
