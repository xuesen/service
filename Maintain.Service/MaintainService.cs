using System;
using System.Collections.Generic;
using System.Linq;
// using IIMes.Services.Maintain.Model;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Core.Model;
using IIMes.Services.Maintain.Interface;

namespace IIMes.Services.Maintain.Services
{
    public partial class MaintainService : IMaintainService
    {
        private readonly IRepository<SProductLine> _repProductLine;

        private readonly IRepository<SEquipment> _repEquipment;

        private readonly IRepository<SPart> _repPart;

        private readonly IRepository<STerminal> _repTerminal;
        private readonly ICommonService _commonService;

        public MaintainService(
                                IRepository<SProductLine> repProductLine,
                                IRepository<SEquipment> repEquipment,
                                IRepository<SPart> repPart,
                                IRepository<STerminal> repTerminal,
                                ICommonService commonService)
                            // ISnoService snoService,
                            // IPublishEndpoint publishEndpoint
        {
            _repProductLine = repProductLine;
            _repEquipment = repEquipment;
            _repPart = repPart;
            _repTerminal = repTerminal;
            _commonService = commonService;
            // _snoService = snoService;
            // this.publishEndpoint = publishEndpoint;
        }

        // 获取生产线，设备下拉框选项
        public IList<CommonDTO> GetCommonInfo(string tableName)
        {
            IList<CommonDTO> commonlst = new List<CommonDTO>();

            // 生产线
            if (tableName.Equals("Line", StringComparison.CurrentCultureIgnoreCase))
            {
                var res = _repProductLine.FindAll().OrderBy(p => p.Code).ToList();
                commonlst = GetCommonLst(res);
            }

            // 设备
            if (tableName.Equals("Equipment", StringComparison.CurrentCultureIgnoreCase))
            {
                var res = _repEquipment.FindAll().OrderBy(p => p.Code).ToList();
                commonlst = GetCommonLst(res);
            }

            return commonlst;
        }

        private IList<CommonDTO> GetCommonLst(dynamic data)
        {
            IList<CommonDTO> commonlst = new List<CommonDTO>();
            if (data != null)
            {
                foreach (var item in data)
                {
                    CommonDTO commondto = new CommonDTO
                    {
                        Id = item.Id,
                        Code = item.Code,
                        Name = item.Name
                    };
                    commonlst.Add(commondto);
                }
            }

            return commonlst;
        }

        public IList<CommonDTO> GetEquipmentbyLine(int lineid)
        {
            return _commonService.GetEquipmentbyLine(lineid);
        }

        public IList<PartsDTO> GetParts()
        {
            var parts = new List<PartsDTO>();
            var res = _repPart.FindAll().OrderBy(p => p.PartNo).ToList();

            for (int i = 0; i < res.Count; i++)
            {
                PartsDTO partitem = new PartsDTO
                {
                    PartId = res[i].Id,
                    PartNo = res[i].PartNo,
                    PartName = res[i].PartName,
                    SPEC1 = res[i].Spec1,
                    SPEC2 = res[i].Spec2
                };
                parts.Add(partitem);
            }

            return parts;
        }

        public IList<PartMarketDTO> GetMarketParts()
        {
            return _commonService.GetMartketPart();
        }

        public IList<PartMarketDTO> GetCatchParts()
        {
            return _commonService.GetCatchPart();
        }

        public IList<CommonDTO> GetSetting(string program)
        {
            IList<CommonDTO> commonlst = new List<CommonDTO>();
            var res = _commonService.GetSetting(program);
            commonlst = GetCommonLst(res);
            return commonlst;
        }
    }
}
