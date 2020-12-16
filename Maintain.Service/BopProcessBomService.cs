// using System.Net.Cache;
// using System;
// using System.Threading;
// using System.Threading.Tasks;
// using IIMes.Services.Core.Model;
// using IIMes.Services.Maintain.Model;
// using MassTransit;
// using MassTransit.Util;
// using IIMes.Services.Core.Message;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using IIMes.Infrastructure.Aspect;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Service;
using IIMes.Services.Core.Model;
using IIMes.Services.Maintain.Interface;
using IIMes.Services.Maintain.Repository;

namespace IIMes.Services.Maintain.Services
{
    public partial class BopProcessBomService : BaseMaintainService<SBopProcessBom, SBopProcessBomDTO>, IBopProcessBomService
    {
        private IRepository<SBopProcessBom> _rep;

        public BopProcessBomService(
                                IRepository<SBopProcessBom> rep,
                                IMapper mapper)
        : base(rep, mapper)
        {
            _rep = rep;
        }

        public object Search(BopProcessRequestDTO requestDto)
        {
            var bopBaseId = requestDto.BopBaseId;
            var processId = requestDto.ProcessId;
            var res = _rep.Query().Where(p => p.BopBase.Id == bopBaseId && p.Process.Id == processId).ToList();
            var ret = Mapper.Map<List<SBopProcessBom>, List<SBopProcessBomDTO>>(res);
            return ret;
        }
    }
}
