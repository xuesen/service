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
using IIMes.Infrastructure.Exception;
using IIMes.Infrastructure.Query;
using IIMes.Infrastructure.Service;
using IIMes.Services.Core.Model;
using IIMes.Services.Maintain.Interface;
using IIMes.Services.Maintain.Repository;

namespace IIMes.Services.Maintain.Services
{
    public partial class BopProcessTestitemService : BaseMaintainService<SBopProcessTestitem, SBopProcessTestitemDTO>, IBopProcessTestitemService
    {
        private IRepository<SBopProcessTestitem> _rep;

        public BopProcessTestitemService(
                                IRepository<SBopProcessTestitem> rep,
                                IMapper mapper)
        : base(rep, mapper)
        {
            _rep = rep;
        }

        public override void Add(SBopProcessTestitemDTO obj)
        {
            CheckDuplication(obj);
            obj.Sequence = GetMaxIndex() + 1;
            base.Add(obj);
        }

        public int GetMaxIndex()
        {
            var sBopProcessTestitem = _rep.Query().OrderByDescending(p => p.Sequence).FirstOrDefault();
            var maxIndex = sBopProcessTestitem == null ? 0 : sBopProcessTestitem.Sequence;
            return maxIndex;
        }

        public object Search(BopProcessRequestDTO requestDto)
        {
            var bopBaseId = requestDto.BopBaseId;
            var processId = requestDto.ProcessId;
            var res = _rep.Query().Where(p => p.BopBaseId == bopBaseId && p.ProcessId == processId).ToList();
            var ret = Mapper.Map<List<SBopProcessTestitem>, List<SBopProcessTestitemDTO>>(res);
            return ret;
        }

        // 同一个bopbase下的同一工序不允许存在相同的测试小项
        protected override void CheckDuplication(SBopProcessTestitemDTO obj)
        {
            var bopBaseId = obj.BopBaseId;
            var processId = obj.ProcessId;
            var testItemId = obj.TestItem.Id;

            var sBopProcessTestitems = _rep.Query().Where(p => p.BopBaseId == bopBaseId && p.ProcessId == processId && p.STestItem.Id == testItemId);
            // 更新时 相同项需要排除它本身
            sBopProcessTestitems = obj.Id == null ? sBopProcessTestitems : sBopProcessTestitems.Where(p => p.Id != obj.Id);

            if (sBopProcessTestitems.Count() > 0)
            {
                throw new BizException("CHK032");
            }
        }

        [Transactional]
        public override void Update(int id, SBopProcessTestitemDTO obj)
        {
            obj.Id = id;
            CheckDuplication(obj);
            base.Update(id, obj);
        }
    }
}
