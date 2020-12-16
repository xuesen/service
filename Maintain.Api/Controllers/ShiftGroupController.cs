using System;
using IIMes.Infrastructure.BaseController;
using IIMes.Infrastructure.Query;
using IIMes.Services.Core.Model;
using IIMes.Services.Maintain.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IIMes.Services.Maintain.Controllers
{
    public class ShiftGroupController : BaseController<SShiftGroup, ShiftGroupDTO>
    {
        public ShiftGroupController(ILogger<SShiftGroup> logger, IShiftGroupService shiftGroupService)
        : base(logger, shiftGroupService)
        {
        }
    }
}