using System.Collections.Generic;
using System.Linq;
using IIMes.Infrastructure.BaseController;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Core.Model;
using IIMes.Services.Maintain.Interface;
using IIMes.Services.Maintain.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IIMes.Services.Maintain.Controllers
{
    public class BarcodeRuleController : BaseController<SBarcodeRule, SBarcodeRuleDTO>
    {
        private readonly IBarcodeRuleService _barcodeRuleService;

        public BarcodeRuleController(ILogger<SBarcodeRule> logger, IBarcodeRuleService barcodeRuleService, IRepository<SBarcodeRule> repBarcodeRule, IRepository<SSetting> repSetting)
        : base(logger, barcodeRuleService)
        {
            _barcodeRuleService = barcodeRuleService;
        }

        [Route("rule/{ruletype}")]
        [HttpGet]
        public virtual ActionResult GetAll([FromRoute] string ruletype)
        {
            var barcoderulelst = _barcodeRuleService.GetAll(ruletype);
            return Ok(barcoderulelst);
        }
    }
}