using System.Collections.Generic;
using AutoMapper;
using IIMes.Infrastructure.Aspect;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Exception;
using IIMes.Infrastructure.Service;
using IIMes.Services.Runtime.Interface;
using IIMes.Services.Runtime.Interface.DTO;
using IIMes.Services.Runtime.Model.Product;
using IIMes.Services.Runtime.Repository;

namespace IIMes.Services.Runtime.Services
{
    public partial class PartService : BaseService<Part, PartItemDTO>, IPartService
    {
        public PartService(IRepository<Part> rep, IMapper mapper)
        : base(rep, mapper)
        {
        }
    }
}
