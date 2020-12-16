using IIMes.Infrastructure.BaseController;
using IIMes.Services.Core.Model;
using IIMes.Services.Maintain.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IIMes.Services.Maintain.Controllers
{
    public class ScrapReasonController : BaseController<SScrapreason, ScrapReasonDTO>
    {
        public ScrapReasonController(ILogger<SScrapreason> logger,  IScrapReasonService scrapReasonService)
        : base(logger, scrapReasonService)
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
