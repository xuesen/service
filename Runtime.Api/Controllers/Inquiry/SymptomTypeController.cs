using System;
using IIMes.Infrastructure.BaseController;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Query;
using IIMes.Services.Core.Model;
using IIMes.Services.Runtime.Interface;
using IIMes.Services.Runtime.Interface.DTO;
using IIMes.Services.Runtime.Model.Quality;
using IIMes.Services.Runtime.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IIMes.Services.Runtime.Controllers
{
    public class SymptomTypeController : BaseController<SymptomType, CommonDTO>
    {
        public SymptomTypeController(ILogger<SymptomType> logger,  ISymptomTypeService symptomtypeService)
        : base(logger, symptomtypeService)
        {
        }
    }
}

