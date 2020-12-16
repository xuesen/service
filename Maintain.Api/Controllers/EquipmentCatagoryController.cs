using IIMes.Infrastructure.BaseController;
using IIMes.Services.Core.Model;
using IIMes.Services.Maintain.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IIMes.Services.Maintain.Controllers
{
    public class EquipmentCatagoryController : BaseController<SEquipmentCatagory, EquipmentCatagoryDTO>
    {
        public EquipmentCatagoryController(ILogger<SEquipmentCatagory> logger,  IEquipmentCatagoryService equipmentCatagoryService)
        : base(logger, equipmentCatagoryService)
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
