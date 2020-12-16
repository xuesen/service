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
    public partial class BarcodeRuleService : BaseMaintainService<SBarcodeRule, SBarcodeRuleDTO>, IBarcodeRuleService
    {
        private IRepository<SBarcodeRule> _repBarcodeRule;
        private IRepository<SSetting> _repSetting;

        public BarcodeRuleService(
                                IRepository<SSetting> repSetting,
                                IRepository<SBarcodeRule> repBarcodeRule,
                                IMapper mapper)
        : base(repBarcodeRule, mapper)
        {
            _repBarcodeRule = repBarcodeRule;
            _repSetting = repSetting;
        }

        protected override void CheckDuplication(SBarcodeRuleDTO obj)
        {
            var bizKeyName = "RuleName";
            var t = typeof(SBarcodeRule);
            var keyProperty = t.GetProperty(bizKeyName);
            if (keyProperty != null)
            {
                var domain = Mapper.Map<SBarcodeRuleDTO, SBarcodeRule>(obj);
                var keyValue = keyProperty.GetValue(domain);
                var duplications = Rep.Query(new QueryParameter() { Name = bizKeyName, Value = keyValue, Fuzzy = false });
                if (duplications != null && duplications.Count > 0)
                {
                    throw new BizException("MTN001", keyValue);
                }
            }
        }

        public IList<SBarcodeRuleDTO> GetAll(string ruletype)
        {
            var setting = _repSetting.GetSetting("RuleType").ToList();
            var ruleid = 0;
            foreach (var item in setting)
            {
                if (item.Code == ruletype)
                {
                    ruleid = item.Id;
                    break;
                }
            }

            var barcoderulelst = _repBarcodeRule.GetBarcodeRule(ruleid).ToList();
            var list = new List<SBarcodeRuleDTO>();
            foreach (var items in barcoderulelst)
            {
                var dto = new SBarcodeRuleDTO
                {
                    Id = items.Id,
                    RuleName = items.RuleName
                };
                list.Add(dto);
            }

            return list;
        }
    }
}
