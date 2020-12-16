using IIMes.Infrastructure.BaseController;
using IIMes.Infrastructure.Query;
using IIMes.Services.Core.Model;
using IIMes.Services.Maintain.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IIMes.Services.Maintain.Controllers
{
    public class SamplingRuleController : BaseController<SSamplingRule, SSamplingRuleDTO>
    {
        public SamplingRuleController(ILogger<SSamplingRule> logger, ISamplingRuleService samplingRuleService)
        : base(logger, samplingRuleService)
        {
        }
    }
}
