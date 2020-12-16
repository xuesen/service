using System;
using IIMes.Infrastructure.BaseController;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Query;
using IIMes.Services.Core.Model;
using IIMes.Services.Maintain.Interface;
using IIMes.Services.Maintain.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IIMes.Services.Maintain.Controllers
{
    public class WoController : BaseController<WoBase, WoDTO>
    {
        private readonly ILogger<WoBase> _logger;

        private readonly IWoService _woService;

        public WoController(ILogger<WoBase> logger, IWoService woService)
        : base(logger, woService)
        {
            _logger = logger;
            _woService = woService;
        }

        // 获取工单列表
        [Route("wos")]
        [HttpPost]
        public ActionResult QueryWos([FromBody]dynamic request)
        {
            WoRequestDTO woRequestdto = request.ToObject<WoRequestDTO>();
            var wolstObj = _woService.Query(woRequestdto);
            return Ok(wolstObj);
        }

        // 新增工单
        [Route("wo")]
        [HttpPost]
        public ActionResult AddWo([FromBody]dynamic request)
        {
            WoBaseRequestDTO wobaseRequestdto = request.ToObject<WoBaseRequestDTO>();
            _woService.AddWo(wobaseRequestdto);
            return Ok(BaseResult<string>.CreateOkResult("ok"));
        }

        // 编辑工单
        [Route("wo/{id}")]
        [HttpPut]
        public ActionResult SaveWo([FromRoute] int id, [FromBody]dynamic request)
        {
            WoBaseRequestDTO wobaseRequestdto = request.ToObject<WoBaseRequestDTO>();
            wobaseRequestdto.Id = id;
            _woService.SaveWo(wobaseRequestdto);
            return Ok(BaseResult<string>.CreateOkResult("ok"));
        }

        // 更新工单状态
        [Route("wostatus")]
        [HttpPost]
        public ActionResult UpdateWoStatus([FromBody]dynamic request)
        {
            WoStatusRequestDTO woRequestdto = request.ToObject<WoStatusRequestDTO>();
            _woService.UpdateWoStatus(woRequestdto);
            return Ok(BaseResult<string>.CreateOkResult("ok"));
        }

        // 上移下移工单
        [HttpPost]
        [Route("sort")]
        public IActionResult SortWo([FromBody]dynamic request)
        {
            WoSortRequestDTO wosortRequestdto = request.ToObject<WoSortRequestDTO>();
            _woService.SortWo(wosortRequestdto);
            return Ok(BaseResult<string>.CreateOkResult("ok"));
        }
    }
}