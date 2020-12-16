using System.Collections.Generic;
using IIMes.Infrastructure.Query;
using IIMes.Infrastructure.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IIMes.Infrastructure.BaseController
{
    [ApiController]
    [Route("api/[controller]")]
    [CustExceptionFilter]
    [CustActionFilter]
    public class BaseController<T, TDTO> : ControllerBase
        where T : DomainObject.DomainObject
        where TDTO : class
    {
        private readonly ILogger<T> logger;

        private readonly IService<T, TDTO> service;

        protected ILogger<T> Logger => logger;

        protected IService<T, TDTO> Service => service;

        public BaseController(ILogger<T> logger, IService<T, TDTO> service)
        {
            this.logger = logger;
            this.service = service;
        }

        [HttpGet]
        public virtual ActionResult Get()
        {
            var ret = Service.GetAll();
            return Ok(ret);
        }

        [Route("{id}")]
        [HttpGet]
        public virtual ActionResult Get([FromRoute] int id)
        {
            var ret = Service.FindById(id);
            if (ret == null)
            {
                return NotFound();
            }

            return Ok(ret);
        }

        [HttpPost]
        public virtual ActionResult Add(TDTO obj)
        {
            Service.Add(obj);
            return Ok(obj);
        }

        [Route("list")]
        [HttpPost]
        public virtual ActionResult AddList(IList<TDTO> obj)
        {
            Service.AddList(obj);
            return Ok(obj);
        }

        [Route("{id}")]
        [HttpPut]
        public virtual ActionResult Update([FromRoute] int id, [FromBody] TDTO obj)
        {
            Service.Update(id, obj);
            return Ok(obj);
        }

        [Route("{id}")]
        [HttpDelete]
        public virtual ActionResult Delete([FromRoute] int id)
        {
            Service.Delete(id);
            return Ok();
        }

        [Route("query")]
        [HttpPost]
        public virtual ActionResult Query([FromBody] QueryParameter query)
        {
            var ret = Service.Query(query);
            return Ok(ret);
        }
    }
}