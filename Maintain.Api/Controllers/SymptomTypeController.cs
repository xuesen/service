using System;
using IIMes.Infrastructure.BaseController;
using IIMes.Infrastructure.Query;
using IIMes.Services.Core.Model;
using IIMes.Services.Maintain.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IIMes.Services.Maintain.Controllers
{
    public class SymptomTypeController : BaseController<SSymptomType, SSymptomTypeDTO>
    {
        public SymptomTypeController(ILogger<SSymptomType> logger, ISymptomTypeService symptomTypeService)
        : base(logger, symptomTypeService)
        {
        }
    }
}