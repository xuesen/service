using System;
using IIMes.Infrastructure.BaseController;
using IIMes.Infrastructure.Query;
using IIMes.Services.Core.Model;
using IIMes.Services.Maintain.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IIMes.Services.Maintain.Controllers
{
    public class SymptomController : BaseController<SSymptom, SSymptomDTO>
    {
        public SymptomController(ILogger<SSymptom> logger, ISymptomService symptomService)
        : base(logger, symptomService)
        {
        }
    }
}