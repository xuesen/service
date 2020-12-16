// using System.Net.Cache;
// using System;
// using System.Threading;
// using System.Threading.Tasks;
// using IIMes.Services.Core.Model;
// using IIMes.Services.Maintain.Model;
// using MassTransit;
// using MassTransit.Util;
// using IIMes.Services.Core.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    public partial class BopProcessLabelService : BaseMaintainService<SBopProcessLabel, SBopProcessLabelDTO>, IBopProcessLabelService
    {
        private IRepository<SBopProcessLabel> _rep;

        public BopProcessLabelService(
                                IRepository<SBopProcessLabel> rep,
                                IMapper mapper)
        : base(rep, mapper)
        {
            _rep = rep;
        }

        public object Search(BopProcessRequestDTO requestDto)
        {
            var bopBaseId = requestDto.BopBaseId;
            var processId = requestDto.ProcessId;
            var res = _rep.Query().Where(p => p.BopBaseId == bopBaseId && p.ProcessId == processId).ToList();
            var ret = Mapper.Map<List<SBopProcessLabel>, List<SBopProcessLabelDTO>>(res);
            return ret;
        }

        [Transactional]
        public override void Update(int id, SBopProcessLabelDTO obj)
        {
            obj.Id = id;
            CheckDuplication(obj);
            base.Update(id, obj);
        }

        // 同一个bopbase下的同一工序打印模板” + "打印机" 是唯一的
        protected override void CheckDuplication(SBopProcessLabelDTO obj)
        {
            var bopBaseId = obj.BopBaseId;
            var processId = obj.ProcessId;
            var printTemplateId = obj.PrintTemplate.Id;
            var triggerId = obj.TriggerId;

            var sBopProcessLabels = _rep.Query().
                Where(p => p.BopBaseId == bopBaseId && p.ProcessId == processId &&
                p.PrintTemplate.Id == printTemplateId && p.SSetting.Id == triggerId);
            // 更新时 相同项需要排除它本身
            sBopProcessLabels = obj.Id == null ? sBopProcessLabels : sBopProcessLabels.Where(p => p.Id != obj.Id);

            if (sBopProcessLabels.Count() > 0)
            {
                throw new BizException("CHK032");
            }
        }
    }
}
