using System;
using IIMes.Infrastructure.BaseController;
using IIMes.Infrastructure.Query;
using IIMes.Services.Core.Model;
using IIMes.Services.Maintain.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IIMes.Services.Maintain.Controllers
{
    public class ShiftController : BaseController<SShift, ShiftDTO>
    {
        public ShiftController(ILogger<SShift> logger, IShiftService shiftService)
        : base(logger, shiftService)
        {
        }
    }
}