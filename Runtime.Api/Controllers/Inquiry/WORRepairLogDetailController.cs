using System;
using IIMes.Infrastructure.BaseController;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Query;
using IIMes.Services.Core.Model;
using IIMes.Services.Runtime.Interface;
using IIMes.Services.Runtime.Interface.DTO;
using IIMes.Services.Runtime.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IIMes.Services.Runtime.Controllers
{
    public class WORRepairLogDetailController : BaseController
    {
        private readonly ILogger<WORRepairLogDetailController> _logger;
        private readonly IWORRepairLogDetailService _worrepairlogdetailService;

        public WORRepairLogDetailController(ILogger<WORRepairLogDetailController> logger, IWORRepairLogDetailService worrepairlogdetailService)
        {
            _logger = logger;
            _worrepairlogdetailService = worrepairlogdetailService;
        }
    }
}