using System;
using IIMes.Infrastructure.BaseController;
using IIMes.Infrastructure.Query;
using IIMes.Services.Core.Model;
using IIMes.Services.Maintain.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IIMes.Services.Maintain.Controllers
{
    public class RepairTypeController : BaseController<SRepairType, SRepairTypeDTO>
    {
        public RepairTypeController(ILogger<SRepairType> logger, IRepairTypeService repairTypeServic)
        : base(logger, repairTypeServic)
        {
        }
    }
}