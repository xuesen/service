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
    public class WORDefectController : BaseController
    {
        private readonly ILogger<WORDefectController> _logger;
        private readonly IWORDefectService _wordefectService;

        public WORDefectController(ILogger<WORDefectController> logger, IWORDefectService wordefectService)
        {
            _logger = logger;
            _wordefectService = wordefectService;
        }
    }
}