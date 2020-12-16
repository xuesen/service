using IIMes.Infrastructure.BaseController;
using IIMes.Services.Maintain.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IIMes.Services.Maintain.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartFsController : BaseController
    {
        private readonly ILogger<PartFsController> _logger;
        private readonly IPartFsService _partfsService;

        // 机型料站维护
        public PartFsController(ILogger<PartFsController> logger,  IPartFsService partfsService)
        {
            _logger = logger;
            _partfsService = partfsService;
        }

        // 查询料站表信息
        [Route("/api/partfs/partfs")]
        [HttpPost]
        public ActionResult GetPartFs([FromBody]dynamic request)
        {
            PartFsDTO partfsdto = request.ToObject<PartFsDTO>();
            var partfsObj = _partfsService.GetPartFsDetail(partfsdto);
            return Ok(partfsObj);
        }

        // 查询机种未开始生产的工单<WIP
        [Route("/api/partfs/checkpartwo/{partid}")]
        [HttpGet]
        public ActionResult CheckPartWo([FromRoute]int partid)
        {
            var partfsObj = _partfsService.CheckPartWo(partid);
            return Ok(partfsObj);
        }

        // //删除料站表信息
        // [Route("/api/partfs/deletepartfs/{id}")]
        // [HttpPost]
        // public ActionResult DeletePartFs([FromRoute] string id)
        // {
        //     _partfsService.DeletePartFs(id);
        //     return Ok(BaseResult<string>.CreateOkResult("ok"));
        // }

        // 检查机型料站导入数据是否已存在数据
        [Route("/api/partfs/checkpartfs")]
        [HttpPost]
        public ActionResult CheckPartFs([FromBody]dynamic partfsinfo)
        {
            PartFsDTO partfsdto = partfsinfo.ToObject<PartFsDTO>();
            var ret = _partfsService.CheckPartFs(partfsdto);
            return Ok(ret);
        }

        // 保存机型料站导入数据
        [Route("/api/partfs/importpartfs")]
        [HttpPost]
        public ActionResult ImportPartFs([FromBody]dynamic partfsinfo)
        {
            PartFsDTO partfsdto = partfsinfo.ToObject<PartFsDTO>();
            _partfsService.ImportPartFs(partfsdto);
            return Ok(BaseResult<string>.CreateOkResult("ok"));
        }
    }
}
