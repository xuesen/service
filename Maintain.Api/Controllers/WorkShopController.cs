using IIMes.Infrastructure.BaseController;
using IIMes.Infrastructure.Query;
using IIMes.Services.Core.Model;
using IIMes.Services.Maintain.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IIMes.Services.Maintain.Controllers
{
    public class WorkShopController : BaseController<SWorkshop, WorkShopDTO>
    {
        public WorkShopController(ILogger<SWorkshop> logger, IWorkShopService workshopService)
        : base(logger, workshopService)
        {
        }
    }
}
