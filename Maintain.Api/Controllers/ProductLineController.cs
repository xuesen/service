using System.Diagnostics;
using IIMes.Infrastructure.BaseController;
using IIMes.Services.Core.Model;
using IIMes.Services.Maintain.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IIMes.Services.Maintain.Controllers
{
    public class ProductLineController : BaseController<SProductLine, PdLineDTO>
    {
        public ProductLineController(ILogger<SProductLine> logger,  IProductLineService productLineService)
        : base(logger, productLineService)
        {
        }
    }
}
