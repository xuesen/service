using System;
using IIMes.Infrastructure.BaseController;
using IIMes.Infrastructure.Query;
using IIMes.Services.Core.Model;
using IIMes.Services.Maintain.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IIMes.Services.Maintain.Controllers
{
    public class BopProcessTestitemController : BaseController<SBopProcessTestitem, SBopProcessTestitemDTO>
    {
        private IBopProcessTestitemService _service;

        public BopProcessTestitemController(ILogger<SBopProcessTestitem> logger, IBopProcessTestitemService service)
        : base(logger, service)
        {
            _service = service;
        }

        // 上移，下移
        [Route("changeSequence/{id}")]
        [HttpPut]
        public virtual ActionResult Update([FromRoute] int id, [FromBody] dynamic obj)
        {
            var x = Service.FindById(id);
            x.Sequence = obj.sequence;
            Service.Update(id, x);
            return Ok(x);
        }

        // 工序下质检项
        [Route("search")]
        [HttpPost]
        public ActionResult Search([FromBody] dynamic request)
        {
            var requestDto = request.ToObject<BopProcessRequestDTO>();
            var ret = _service.Search(requestDto);
            return Ok(ret);
        }
    }
}