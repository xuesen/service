using System.Collections.Generic;
using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Exception;
using IIMes.Services.Core.Model;
using IIMes.Services.Maintain.Interface;
using IIMes.Services.Maintain.Repository;

namespace IIMes.Services.Maintain.Services
{
    public partial class CommonService : ICommonService
    {
        private readonly IRepository<STerminal> _repTerminal;
        private readonly IRepository<SPart> _repPart;
        private readonly IRepository<SEquipment> _repEquipment;
        private readonly IRepository<SSetting> _repSetting;
        private readonly IRepository<WoBase> _repWobase;
        private readonly IRepository<WoFsDetail> _repWoFsDetail;
        private readonly IRepository<WoFsSub> _repWoFsSub;
        private readonly IRepository<SProcess> _repProcess;
        private readonly IRepository<SWoStatus> _repWoStatus;
        private readonly IRepository<SBomItem> _repBomItem;

        public CommonService(
                                IRepository<STerminal> repTerminal,
                                IRepository<SPart> repPart,
                                IRepository<SEquipment> repEquipment,
                                IRepository<SSetting> repSetting,
                                IRepository<WoBase> repWobase,
                                IRepository<WoFsDetail> repWoFsDetail,
                                IRepository<WoFsSub> repWoFsSub,
                                IRepository<SProcess> repProcess,
                                IRepository<SWoStatus> repWoStatus,
                                IRepository<SBomItem> repBomItem)
        {
            _repTerminal = repTerminal;
            _repPart = repPart;
            _repEquipment = repEquipment;
            _repSetting = repSetting;
            _repWobase = repWobase;
            _repWoFsDetail = repWoFsDetail;
            _repWoFsSub = repWoFsSub;
            _repProcess = repProcess;
            _repWoStatus = repWoStatus;
            _repBomItem = repBomItem;
        }

        public WoBase GetWo(string workorder)
        {
            var wobase = _repWobase.GetWo(workorder);
            if (wobase == null)
            {
                // 工单不存在
                throw new BizException("CHK004");
            }

            return wobase;
        }

        public SPart GetPart(string partno)
        {
            return _repPart.GetPart(partno);
        }

        public SEquipment GetEquipment(string equipmentcode)
        {
            var equipment = _repEquipment.GetEquipment(equipmentcode);
            if (equipment == null)
            {
                // 设备不存在
                // throw new Exception("设备不存在");
                throw new BizException("CHK004");
            }

            return equipment;
        }

        public STerminal GetTerminal(string terminalcode)
        {
            var terminal = _repTerminal.GetTerminal(terminalcode);
            if (terminal == null)
            {
                // 工作站不存在
                throw new BizException("CHK012");
            }

            return terminal;
        }

        public IQueryable<SSetting> GetSetting(string program)
        {
            return _repSetting.GetSetting(program);
        }

        public IList<WoFsDetail> GetWoFsDetail(int wofsid)
        {
            return _repWoFsDetail.GetWoFsDetail(wofsid).ToList();
        }

        public void DeleteWoFsDetail(int id)
        {
            _repWoFsDetail.DeleteWoFsDetail(id);
        }

        public IList<WoFsSub> GetWoFsSub(int wofsdetailid)
        {
            return _repWoFsSub.GetWoFsSub(wofsdetailid).ToList();
        }

        public SProcess GetProcess(string processcode)
        {
            return _repProcess.GetProcess(processcode);
        }

        public SWoStatus GetWoStaus(string name)
        {
            return _repWoStatus.GetWoStaus(name);
        }

        public IList<CommonDTO> GetEquipmentbyLine(int lineid)
        {
            var parameters = new Dictionary<string, object>
                                {
                                    { "lineId", lineid }
                                };
            List<CommonDTO> commonlst = _repTerminal.ExecNamedSqlQuery<CommonDTO>("GetEquipmentbyLine", parameters.ToArray()).ToList();
            return commonlst;
        }

        public IList<PartMarketDTO> GetMartketPart()
        {
            List<PartMarketDTO> partlst = _repPart.ExecNamedSqlQuery<PartMarketDTO>("GetMarketPart").ToList();
            return partlst;
        }

        public IList<PartMarketDTO> GetCatchPart()
        {
            List<PartMarketDTO> partlst = _repPart.ExecNamedSqlQuery<PartMarketDTO>("GetCatchPart").ToList();
            return partlst;
        }

        public IList<BomItemDataDTO> GetBomItemsbyPartid(int partid)
        {
            var parameters = new Dictionary<string, object>
                                {
                                    { "partId", partid }
                                };
            List<BomItemDataDTO> bomitemlst = _repBomItem.ExecNamedSqlQuery<BomItemDataDTO>("GetBomItemsbyPartid", parameters.ToArray()).ToList();
            return bomitemlst;
        }

        public IList<CommonWoFsDetailDTO> GetWoFsDetailInfo(int wofsid)
        {
            IList<CommonWoFsDetailDTO> wofsdetaildtolst = new List<CommonWoFsDetailDTO>();
            IList<WoFsDetail> wofsdetailst = GetWoFsDetail(wofsid).ToList();
            foreach (var item in wofsdetailst)
            {
                var part = _repPart.Get(item.ItemPartId);
                // 替代料
                var wofssubs = GetWoFsSub(item.Id).ToList();
                CommonWoFsDetailDTO wofsdetaildto = new CommonWoFsDetailDTO
                {
                    Id = item.Id,
                    Station = item.Station,
                    ItemPartId = item.ItemPartId,
                    ItemPartNo = part.PartNo,
                    ItemPartName = part.PartName,
                    ItemCount = item.ItemCount
                };
                if (wofssubs.Count > 0)
                {
                    IList<CommonWoFsSubDTO> woFsSubDTOs = new List<CommonWoFsSubDTO>();
                    foreach (var sub in wofssubs)
                    {
                        CommonWoFsSubDTO wofssubdto = new CommonWoFsSubDTO();
                        var subpart = _repPart.Get(sub.SubPartId);
                        wofssubdto.SubPartId = sub.SubPartId;
                        wofssubdto.SubPartNo = subpart.PartNo;
                        wofssubdto.SubPartName = subpart.PartName;
                        woFsSubDTOs.Add(wofssubdto);
                    }

                    wofsdetaildto.WoFsSub = woFsSubDTOs;
                }

                wofsdetaildto.Location = item.Location;
                wofsdetaildto.FeederType = item.FeederType;
                wofsdetaildto.FeederSpec = item.FeederSpec;
                wofsdetaildtolst.Add(wofsdetaildto);
            }

            return wofsdetaildtolst;
        }

        public IList<EqualWoFsDetailDTO> ChangeEqualWoFsDetail(IList<CommonWoFsDetailDTO> commonwofsdetaildto)
        {
                IList<EqualWoFsDetailDTO> equalWoFsDetailDtoLst = new List<EqualWoFsDetailDTO>();
                foreach (CommonWoFsDetailDTO item in commonwofsdetaildto)
                {
                    EqualWoFsDetailDTO detaildto = new EqualWoFsDetailDTO
                    {
                        ItemPartId = item.ItemPartId,
                        ItemCount = item.ItemCount,
                        Location = item.Location,
                        FeederType = item.FeederType,
                        FeederSpec = item.FeederSpec
                    };
                    IList<int> sublst = new List<int>();
                    var fssublst = _repWoFsSub.GetWoFsSub(item.ItemPartId).ToList();
                    if (fssublst.Count > 0)
                    {
                        foreach (var sub in fssublst)
                        {
                            sublst.Add(sub.SubPartId);
                        }
                    }
                    else
                    {
                        // modify ITC-1753-0016
                        // 没有替代料
                        sublst.Add(-1);
                    }

                    detaildto.FsSubLst = sublst;
                    equalWoFsDetailDtoLst.Add(detaildto);
                }

                return equalWoFsDetailDtoLst;
        }

        public bool IsEqualWoFsDetail(IList<EqualWoFsDetailDTO> listA, IList<EqualWoFsDetailDTO> listB)
        {
            bool ret = false;
            if (listA.Count != listB.Count)
            {
                 return ret;
            }

            var num = 0;
            foreach (EqualWoFsDetailDTO item in listA)
            {
                foreach (EqualWoFsDetailDTO items in listB)
                {
                    if (items.FeederSpec == item.FeederSpec &&
                        items.FeederType == item.FeederType &&
                        items.ItemCount == item.ItemCount &&
                        items.ItemPartId == item.ItemPartId &&
                        items.Location == item.Location &&
                        items.Station == item.Station &&
                        items.FsSubLst.Count == item.FsSubLst.Count)
                    {
                        if (items.FsSubLst.Count == item.FsSubLst.Count && items.FsSubLst.Count(t => !item.FsSubLst.Contains(t)) == 0)
                        {
                            num++;
                            break;
                        }
                    }
                }
            }

            if (num == listA.Count)
            {
                ret = true;
            }

            return ret;
        }
    }
}
