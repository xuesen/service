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
    public partial class SamplingPlanService : BaseMaintainService<SSamplingPlan, SSamplingPlanDTO>, ISamplingPlanService
    {
        public SamplingPlanService(
                                IRepository<SSamplingPlan> rep,
                                IMapper mapper)
        : base(rep, mapper)
        {
        }

        protected override void CheckDuplication(SSamplingPlanDTO obj)
        {
            var bizKeyName = "Name";
            var t = typeof(SSamplingPlan);
            var keyProperty = t.GetProperty(bizKeyName);
            if (keyProperty != null)
            {
                var domain = Mapper.Map<SSamplingPlanDTO, SSamplingPlan>(obj);
                var keyValue = keyProperty.GetValue(domain);
                var duplications = Rep.Query(new QueryParameter() { Name = bizKeyName, Value = keyValue, Fuzzy = false });
                if (duplications != null && duplications.Count > 0)
                {
                    throw new BizException("MTN001", keyValue);
                }
            }
        }
    }
}
