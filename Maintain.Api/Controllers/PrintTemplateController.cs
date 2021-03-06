﻿using IIMes.Infrastructure.BaseController;
using IIMes.Infrastructure.Query;
using IIMes.Services.Core.Model;
using IIMes.Services.Maintain.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IIMes.Services.Maintain.Controllers
{
    public class PrintTemplateController : BaseController<SPrintTemplate, SPrintTemplateDTO>
    {
        public PrintTemplateController(ILogger<SPrintTemplate> logger, IPrintTemplateService printTemplateService)
        : base(logger, printTemplateService)
        {
        }
    }
}
