using IIMes.Infrastructure.BaseController;
using IIMes.Services.Core.Model;
using IIMes.Services.Maintain.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IIMes.Services.Maintain.Controllers
{
    public class FactoryController : BaseController<SFactory, FactoryDTO>
    {
        public FactoryController(ILogger<SFactory> logger,  IFactoryService factoryService)
        : base(logger, factoryService)
        {
        }

        /*
                [Route("/input/{sno}")]
                [HttpGet]
                public ActionResult InputSn(string sno)
                {
                    var snoObj = ((IProductLineService)this.Service).InputSn(new InputSnRequestDTO() { Sno = sno });
                    return Ok(snoObj);
                } */
    }
}
