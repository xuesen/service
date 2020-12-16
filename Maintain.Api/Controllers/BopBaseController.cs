using System;
using IIMes.Infrastructure.BaseController;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Query;
using IIMes.Services.Core.Model;
using IIMes.Services.Maintain.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IIMes.Services.Maintain.Controllers
{
    public class BopBaseController : BaseController<SBopBase, SBopBaseDTO>
    {
        private readonly IBopBaseService _bopBaseService;

        private readonly IRepository<SBopBase> _rep;

        public BopBaseController(
            ILogger<SBopBase> logger,
            IBopBaseService bopBaseService,
            IRepository<SBopBase> rep)
        : base(logger, bopBaseService)
        {
            _bopBaseService = bopBaseService;
            _rep = rep;
        }

        [Route("getPart")]
        [HttpPost]
        public virtual ActionResult GetPart()
        {
            var ret = _bopBaseService.GetPart();
            return Ok(ret);
        }

        [Route("getLastestVersionWorkflow/{wordkflowid}")]
        [HttpPost]
        public virtual ActionResult GetLastestVersionWorkflow([FromRoute] int wordkflowid)
        {
            var ret = _bopBaseService.GetLastestVersionWorkflow(wordkflowid);
            return Ok(ret);
        }

        [Route("getBomVersion/{partid}")]
        [HttpPost]
        public virtual ActionResult GetBomVersion([FromRoute] int partid)
        {
            var ret = _bopBaseService.GetBomVersion(partid);
            return Ok(ret);
        }

        [Route("getBomData")]
        [HttpPost]
        public virtual ActionResult GetBomData([FromBody] dynamic request)
        {
            var ret = _bopBaseService.GetBomData(request);
            return Ok(ret);
        }

        [Route("getPartInofo")]
        [HttpPost]
        public virtual ActionResult GetPartInofo([FromBody] dynamic request)
        {
            var ret = _bopBaseService.GetPartInofo(request);
            return Ok(ret);
        }

        [Route("callbopbasesp")]
        [HttpPost]
        public virtual ActionResult CallBopBaseSP([FromBody] BopBaseRequestDTO request)
        {
            _bopBaseService.CallBopBaseSP(request);
            return Ok();
        }

        [Route("UpdateUdt/{id}")]
        [HttpPost]
        public virtual ActionResult UpdateUdt([FromRoute] int id)
        {
            // 更新Udt
            _bopBaseService.UpdateUdt(id);
            return Ok();
        }

        // 发布前检查
        [Route("checkBeforePublish/{bopBaseId}")]
        [HttpPost]
        public virtual ActionResult CheckBeforePublish([FromRoute] int bopBaseId)
        {
            var ret = _bopBaseService.CheckBeforePublish(bopBaseId);
            return Ok(ret);
        }

        // 发布
        [Route("Publish")]
        [HttpPost]
        public virtual ActionResult Publish(BopBaseRequestDTO request)
        {
            var result = _bopBaseService.Publish(request);
            return Ok(result);
        }

        [Route("getKeyPart/{familyId}")]
        [HttpPost]
        public virtual ActionResult GetKeyPart(int familyId)
        {
            var ret = _bopBaseService.GetKeyPart(familyId);
            return Ok(ret);
        }
    }
}
