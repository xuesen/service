using System;
using IIMes.Infrastructure.BaseController;
using IIMes.Infrastructure.Query;
using IIMes.Services.Core.Model;
using IIMes.Services.Maintain.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IIMes.Services.Maintain.Controllers
{
    public class CauseController : BaseController<SCause, SCauseDTO>
    {
        public CauseController(ILogger<SCause> logger, ICauseService causeService)
        : base(logger, causeService)
        {
        }
    }
}