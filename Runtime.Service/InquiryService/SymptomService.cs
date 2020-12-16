using System.Collections.Generic;
using AutoMapper;
using IIMes.Infrastructure.Aspect;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Exception;
using IIMes.Infrastructure.Service;
using IIMes.Services.Runtime.Interface;
using IIMes.Services.Runtime.Interface.DTO;
using IIMes.Services.Runtime.Model.Quality;
using IIMes.Services.Runtime.Repository;

namespace IIMes.Services.Runtime.Services
{
    public partial class SymptomService : BaseService<Symptom, CommonDTO>, ISymptomService
    {
        public SymptomService(IRepository<Symptom> rep, IMapper mapper)
        : base(rep, mapper)
        {
        }
    }
}
