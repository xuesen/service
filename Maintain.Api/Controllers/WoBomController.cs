using System.Diagnostics;
using IIMes.Infrastructure.BaseController;
using IIMes.Services.Core.Model;
using IIMes.Services.Maintain.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IIMes.Services.Maintain.Controllers
{
    public class WoBomController : BaseController<WoBom, WoBomDTO>
    {
        public WoBomController(ILogger<WoBom> logger,  IWoBomService woBomService)
        : base(logger, woBomService)
        {
        }
    }
}
