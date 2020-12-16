using System;
using IIMes.Infrastructure.BaseController;
using IIMes.Infrastructure.Query;
using IIMes.Services.Core.Model;
using IIMes.Services.Maintain.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IIMes.Services.Maintain.Controllers
{
    public class TestItemController : BaseController<STestItem, STestItemDTO>
    {
        public TestItemController(ILogger<STestItem> logger, ITestItemService testItemService)
        : base(logger, testItemService)
        {
        }
    }
}