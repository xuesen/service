using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using IIMes.Infrastructure.Aspect;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Exception;
using IIMes.Infrastructure.Service;
using IIMes.Services.Core.Model;
using IIMes.Services.Maintain.Interface;
using IIMes.Services.Maintain.Model;
using IIMes.Services.Maintain.Repository;

namespace IIMes.Services.Maintain.Services
{
    public partial class WoBaseService : BaseMaintainService<WoBase, WoBaseDTO>, IWoBaseService
    {
        public WoBaseService(
                            IRepository<WoBase> rep,
                            IMapper mapper)
        : base(rep, mapper)
        {
        }
    }
}
