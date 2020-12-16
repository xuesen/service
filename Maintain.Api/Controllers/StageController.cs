using IIMes.Infrastructure.BaseController;
using IIMes.Services.Core.Model;
using IIMes.Services.Maintain.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IIMes.Services.Maintain.Controllers
{
    public class StageController : BaseController<SStage, StageDTO>
    {
        public StageController(ILogger<SStage> logger,  IStageService stageService)
        : base(logger, stageService)
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
