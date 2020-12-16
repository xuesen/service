using System.Collections.Generic;
using AutoMapper;
using IIMes.Infrastructure.Aspect;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Exception;
using IIMes.Infrastructure.Service;
using IIMes.Services.Runtime.Interface;
using IIMes.Services.Runtime.Interface.DTO;
using IIMes.Services.Runtime.Model.Plant;
using IIMes.Services.Runtime.Repository;

namespace IIMes.Services.Runtime.Services
{
    public partial class TerminalService : BaseService<Terminal, TerminalDTO>, ITerminalService
    {
        public TerminalService(IRepository<Terminal> rep, IMapper mapper)
        : base(rep, mapper)
        {
        }
    }
}
