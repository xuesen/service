using IIMes.Infrastructure.BaseController;
using IIMes.Infrastructure.Query;
using IIMes.Services.Core.Model;
using IIMes.Services.Maintain.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IIMes.Services.Maintain.Controllers
{
    public class SamplingPlanDetailController : BaseController<SSamplingPlanDetail, SSamplingPlanDetailDTO>
    {
        public SamplingPlanDetailController(ILogger<SSamplingPlanDetail> logger, ISamplingPlanDetailService samplingPlanDetailService)
        : base(logger, samplingPlanDetailService)
        {
        }
    }
}
