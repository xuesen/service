using System;
using IIMes.Infrastructure.BaseController;
using IIMes.Infrastructure.Query;
using IIMes.Services.Core.Model;
using IIMes.Services.Maintain.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IIMes.Services.Maintain.Controllers
{
    public class DutyController : BaseController<SDuty, SDutyDTO>
    {
        public DutyController(ILogger<SDuty> logger, IDutyService dutyService)
        : base(logger, dutyService)
        {
        }
    }
}