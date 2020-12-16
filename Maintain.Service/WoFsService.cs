using System.Collections.Generic;
using System.Linq;
using IIMes.Infrastructure.Aspect;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Exception;
using IIMes.Infrastructure.Hibernate.Data;
using IIMes.Infrastructure.Hibernate.MSSQL;
using IIMes.Services.Core.Model;
using IIMes.Services.Maintain.Interface;
using IIMes.Services.Maintain.Model;
using IIMes.Services.Maintain.Repository;

namespace IIMes.Services.Maintain.Services
{
    public partial class WoFsService : IWoFsService
    {
        private readonly IRepository<WoBase> _repWoBase;
        private readonly IRepository<SPart> _repPart;
        private readonly IRepository<SPartFs> _repPartFs;
        private readonly IRepository<SPartFsDetail> _repPartFsDetail;
        private readonly IRepository<SEquipment> _repSEquipment;
        private readonly IRepository<SWoStatus> _repSWoStatus;
        private readonly IRepository<WoFs> _repWoFs;
        private readonly IRepository<WoFsDetail> _repWoFsDetail;
        private readonly IRepository<WoFsSub> _repWoFsSub;
        private readonly IRepository<FsStatus> _repFsStatus;
        private readonly IRepository<SBomItem> _repBomItem;
        private readonly IRepository<WoBom> _repWoBom;
        private readonly ICommonService _commonService;

        public WoFsService(
                            IRepository<WoBase> repWoBase,
                            IRepository<SPart> repPart,
                            IRepository<SPartFs> repPartFs,
                            IRepository<SPartFsDetail> repPartFsDetail,
                            IRepository<SEquipment> repSEquipment,
                            IRepository<SWoStatus> repSWoStatus,
                            IRepository<WoFs> repWoFs,
                            IRepository<WoFsDetail> repWoFsDetail,
                            IRepository<WoFsSub> repWoFsSub,
                            IRepository<FsStatus> repFsStatus,
                            IRepository<SBomItem> repBomItem,
                            IRepository<WoBom> repWoBom,
                            ICommonService commonService)
                            // ISnoService snoService,
                            // IPublishEndpoint publishEndpoint
        {
            _repWoBase = repWoBase;
            _repPart = repPart;
            _repPartFs = repPartFs;
            _repPartFsDetail = repPartFsDetail;
            _repSEquipment = repSEquipment;
            _repSWoStatus = repSWoStatus;
            _repWoFs = repWoFs;
            _repWoFsDetail = repWoFsDetail;
            _repWoFsSub = repWoFsSub;
            _repFsStatus = repFsStatus;
            _repBomItem = repBomItem;
            _repWoBom = repWoBom;
            _commonService = commonService;
            // _snoService = snoService;
            // this.publishEndpoint = publishEndpoint;
        }

        // 查询工单料站表信息，界面显示的上面表格主要信息
        public IList<WoFsInfoDTO> GetWoFsInfo(WoFsRequestDTO wofsrequestdto)
        {
            var wofsinfolst = new List<WoFsInfoDTO>();
            if (wofsrequestdto != null)
            {
                var parameters = new Dictionary<string, object>
                                    {
                                        { "workOrder", wofsrequestdto.WorkOrder },
                                        // { "lineId", wofsrequestdto.PdLineId },
                                        { "sideCode", wofsrequestdto.Side },
                                        { "equipmentId", wofsrequestdto.EquipmentId }
                                    };
                wofsinfolst = _repWoFs.ExecNamedSqlQuery<WoFsInfoDTO>("GetWoFsInfo", parameters.ToArray()).ToList();
                // var wobase = _commonService.GetWo(wofsrequestdto.WorkOrder);
                // var wofslist = _repWoFs.GetWoFslist(wofsrequestdto.WorkOrder, wofsrequestdto.PdLineId, wofsrequestdto.EquipmentId);
                // foreach (var item in wofslist)
                // {
                //     var wofsinfo = new WoFsInfoDTO();
                //     wofsinfo.WorkOrder = item.WorkOrder;
                //     wofsinfo.Qty = (int)wobase.TargetQty;
                //     wofsinfo.PartId = item.PartId;
                //     wofsinfo.PartNo = wobase.SPart.PartNo;
                //     wofsinfo.WoStatus = wobase.SWoStatus.Name;
                //     wofsinfo.EquipmentId = item.EquipmentId;
                //     wofsinfo.EquipmentCode = _repSEquipment.Get(item.EquipmentId).Code;
                //     wofsinfo.Side = item.Side;
                //     wofsinfo.WoFsId = item.Id;
                //     wofsinfolst.Add(wofsinfo);
                // }
            }

            return wofsinfolst;
        }

        // 已上料工单不能编辑、删除和加载：WO_FS.status不等于“未上料”
        public bool CheckWoStatus(int wofsid)
        {
            bool ret = true;

            var wofs = _repWoFs.Get(wofsid);
            if (wofs == null)
            {
                // Status: "-1": "未上料", "0": "已下料", "1": "已上料"
                ret = false;
                throw new BizException("CHK033");
            }

            var fsstatus = _repFsStatus.GetWoFs(wofs.WorkOrder);
            if (fsstatus != null)
            {
                if (fsstatus.Status == 1)
                {
                    ret = false;
                    throw new BizException("CHK012");
                }
            }

            return ret;
        }

        public IList<CommonWoFsDetailDTO> GetWoFsDetailInfo(int wofsid)
        {
            IList<CommonWoFsDetailDTO> wofsdetaildtolst;
            wofsdetaildtolst = _commonService.GetWoFsDetailInfo(wofsid);
            return wofsdetaildtolst;
        }

        // 检查该工单料站表中是否已经导入过数据，如已经导入过数据提示是否覆盖
        public bool CheckWoFsData(ImportWoFsRequestDTO requestdto)
        {
            var ret = false;
            if (requestdto != null)
            {
                // ImportWoFsRequestDTO requestdto = request.ToObject<ImportWoFsRequestDTO>();
                var wofs = _repWoFs.CheckWoFsData(requestdto.WorkOrder, requestdto.PdLineId, requestdto.EquipmentId, requestdto.Side);

                if (wofs != null)
                {
                    ret = true;
                }
            }

            return ret;
        }

        [Transactional]
        public void ImportPartFs2WoFs(ImportWoFsRequestDTO requestdto)
        {
            // IList<WoFsDetailDTO>  wofsdetaildtolst = new List<WoFsDetailDTO>();
            if (requestdto != null)
            {
                // ImportWoFsRequestDTO requestdto = request.ToObject<ImportWoFsRequestDTO>();
                // 工单料站加载时，检查料站里的料是否都在工单BOM中
                // SPartFs partfs = CheckWoBOM(requestdto);
                var wobase = _commonService.GetWo(requestdto.WorkOrder);
                PartFsDTO partfsdto = new PartFsDTO
                {
                    PdLineId = requestdto.PdLineId,
                    PartId = wobase.SPart.Id,
                    EquipmentId = requestdto.EquipmentId,
                    Side = requestdto.Side
                };
                var partfs = _repPartFs.GetPartFs(partfsdto);
                if (partfs == null)
                {
                    // 机型料站中无数据
                    throw new BizException("CHK009");
                }

                // 如果WO_FS表中已有该工单的数据需要先删除WO_FS, WO_FS_DETAIL, WO_FS_SUB(如有替代料)的数据再进行导入
                DeleteOldWoFsData(requestdto);
                // 保存数据：工单料站表对应数据表：WO_FS、WO_FS_DETAIL、WO_FS_SUB，并把工单BOM的替代料同步到工单料站中
                // var wofsdto = ImportDataToWoFsObj(requestdto);
                Dictionary<string, int> iddic = SaveWoFs(requestdto, partfs);

                // 保存数据：WO_FS_DETAIL
                SaveWoFsDetail(iddic);
                // 保存数据：替代料
                // SaveWoFsSub(requestdto.WorkOrder, iddic);
            }
        }

        // 如果WO_FS表中已有该工单的数据需要先删除WO_FS, WO_FS_DETAIL, WO_FS_SUB(如有替代料)的数据再进行导入
        private void DeleteOldWoFsData(ImportWoFsRequestDTO data)
        {
            var wofs = _repWoFs.CheckWoFsData(data.WorkOrder, data.PdLineId, data.EquipmentId, data.Side);
            if (wofs != null)
            {
                _repWoFs.Delete(wofs);
                var wofsdetaillst = _commonService.GetWoFsDetail(wofs.Id);
                if (wofsdetaillst.Count > 0)
                {
                    for (var i = 0; i < wofsdetaillst.Count; i++)
                    {
                        var wofssublst = _commonService.GetWoFsSub(wofsdetaillst[i].Id);
                        _repWoFsDetail.Delete(wofsdetaillst[i]);
                        if (wofssublst.Count > 0)
                        {
                            for (var j = 0; j < wofssublst.Count; j++)
                            {
                                _repWoFsSub.Delete(wofssublst[j]);
                            }
                        }
                    }
                }
            }
        }

        private void SaveWoFsSub(string workOrder, Dictionary<string, int> idDic)
        {
            int wofsid = idDic["wofsid"];
            var wotfsdetail = _commonService.GetWoFsDetail(wofsid);
            IList<WoBomPartDTO> wobomlst;
            wobomlst = GetWoBomSub(workOrder);
            foreach (var item in wotfsdetail)
            {
                if (wobomlst.Count > 0)
                {
                    for (var i = 0; i < wobomlst.Count; i++)
                    {
                        if (item.ItemPartId == wobomlst[i].ItemPart.Id)
                        {
                            for (var j = 0; j < wobomlst[i].SubPart.Count; j++)
                            {
                                WoFsSub woFsSub = new WoFsSub
                                {
                                    WoFsDetailId = item.Id,
                                    SubPartId = wobomlst[i].SubPart[j].Id
                                };
                                _repWoFsSub.Add(woFsSub);
                            }
                        }
                    }
                }
            }
        }

        private ImportWoFsDTO ImportDataToWoFsObj(dynamic request)
        {
            // 加载机型料站表
            ImportWoFsDTO importwofsdto = request.ToObject<ImportWoFsDTO>();
            return importwofsdto;
        }

        // 工单料站加载时，检查料站里的料是否都在工单BOM中
        private SPartFs CheckWoBOM(ImportWoFsRequestDTO data)
        {
            SPartFs partfs = new SPartFs();
            // IList<PartFsDetailDTO> partfsdetaillst = new List<PartFsDetailDTO>();
            if (data != null)
            {
                var wobase = _commonService.GetWo(data.WorkOrder);
                PartFsDTO partfsdto = new PartFsDTO
                {
                    PdLineId = data.PdLineId,
                    PartId = wobase.SPart.Id,
                    EquipmentId = data.EquipmentId,
                    Side = data.Side
                };
                // 每次都获取S_PART_FS表中的最新数据，CREATE_TIME最新的一笔数据
                var partfstmp = _repPartFs.GetPartFs(partfsdto);
                var partfsdetaillst = _repPartFsDetail.GetPartFsDetail(partfstmp.Id).ToList();
                var bomitemlst = _repWoBom.FindAll();
                var flag = false;
                foreach (var item in partfsdetaillst)
                {
                    flag = false;
                    foreach (var bomitem in bomitemlst)
                    {
                        WoBom wobom = new WoBom();
                        if (item.ItemPartId == bomitem.SSubPart.Id)
                        {
                            flag = true;
                        }
                    }

                    if (flag == false)
                    {
                        // 料站里的料不在工单BOM中
                        throw new BizException("CHK011");
                        // throw new Exception("CHK005", item.ItemPartCode);
                    }
                }

                partfs = partfstmp;
            }

            return partfs;
        }

        private Dictionary<string, int> SaveWoFs(ImportWoFsRequestDTO wofsdto, SPartFs partfs)
        {
            // int status = (int)FsStatusCode.NONE;
            WoFs wofs = new WoFs
            {
                // var partfs = GetPartFs(wofsdto);
                // PartFsNo = partfs.Id.ToString(),
                PartFsId = partfs.Id.ToString(),
                WorkOrder = wofsdto.WorkOrder,
                PartId = partfs.PartId,
                Side = partfs.Side,
                EquipmentId = partfs.EquipmentId,
                // -1:"未上料"," 0:"已下料", 1:"已上料",
                // Status = status.ToString(),
                EmployeeCode = wofsdto.EmployeeCode,
                CreateTime = ((Repository<WoFs>)_repWoFs).GetDate<WoFs>(),
                UpdateTime = ((Repository<WoFs>)_repWoFs).GetDate<WoFs>()
            };
            _repWoFs.Add(wofs);
            var wofsid = _repWoFs.GetWoFsByWoFs(wofs).Id;
            var partfsid = partfs.Id;
            Dictionary<string, int> idDic = new Dictionary<string, int>
            {
                { "wofsid", wofsid },
                { "partfsid", partfsid }
            };

            return idDic;
        }

/*
        private SPartFs GetPartFs(ImportWoFsRequestDTO wofsdto)
        {
            SPartFs partfs = new SPartFs();
            PartFsDTO partFsDTO = new PartFsDTO();
            partFsDTO.PdLineId = wofsdto.PdLineId;
            partFsDTO.PartId = wofsdto.PartId;
            partFsDTO.EquipmentId = wofsdto.EquipmentId;
            partFsDTO.Side = wofsdto.Side;
            partfs = _repPartFs.GetPartFs(partFsDTO);
            return partfs;
        } */

        private void SaveWoFsDetail(Dictionary<string, int> dicId)
        {
            var partfsdetail = GetPartFsDetail(dicId["partfsid"]);

            foreach (var item in partfsdetail)
            {
                WoFsDetail woFsDetail = new WoFsDetail
                {
                    WoFsId = dicId["wofsid"].ToString(),
                    Station = item.Station,
                    ItemPartId = item.ItemPartId,
                    ItemCount = item.ItemCount,
                    Location = item.Location,
                    FeederType = item.FeederType,
                    FeederSpec = item.FeederSpec
                };

                _repWoFsDetail.Add(woFsDetail);
            }
        }

        private IList<SPartFsDetail> GetPartFsDetail(int partfsid)
        {
            IList<SPartFsDetail> partfsdetaillst = _repPartFsDetail.GetPartFsDetail(partfsid).ToList();

            return partfsdetaillst;
        }

        // 替代料
        private void SaveWoFsSub()
        {
        }

        public void SaveWoFsDetailInfo(WoFsDetailRequestDTO wofsdetailrequest, int type)
        {
            if (wofsdetailrequest != null)
            {
                var checkwofs = _repWoFsDetail.GetWoFsByStationItemPartId(wofsdetailrequest.WoFsDetail.Station, wofsdetailrequest.WoFsDetail.ItemPartId);
                if (checkwofs != null && type == 0)
                {
                    // 新增时，(站位与物料)数据已存在
                    throw new BizException("CHK037");
                }

                // WoFsDetailRequestDTO wofsdetailrequest = request.ToObject<WoFsDetailRequestDTO>();
                // WO_FS_DETAIL表：WO_FS_ID, STATION, ITEM_PART_ID, ITEM_COUNT, LOCATION, FEEDER_TYPE, FEEDER_SPEC
                // WO_FS_SUB表：WO_FS_DETAIL_ID,  SUB_PART_ID
                // Action: "Add" -> 新增; "Edit": 编辑
                WoFsDetail woFsDetail = new WoFsDetail
                {
                    WoFsId = wofsdetailrequest.WoFsDetail.WoFsId,
                    Station = wofsdetailrequest.WoFsDetail.Station,
                    ItemPartId = wofsdetailrequest.WoFsDetail.ItemPartId,
                    ItemCount = wofsdetailrequest.WoFsDetail.ItemCount,
                    Location = wofsdetailrequest.WoFsDetail.Location,
                    FeederType = wofsdetailrequest.WoFsDetail.FeederType,
                    FeederSpec = wofsdetailrequest.WoFsDetail.FeederSpec
                };

                if (wofsdetailrequest.Action == "Add")
                {
                    var id = _repWoFsDetail.Add(woFsDetail);

                    if (wofsdetailrequest.SubPartIdLst.Count > 0)
                    {
                        for (var j = 0; j < wofsdetailrequest.SubPartIdLst.Count; j++)
                        {
                            WoFsSub wofssub = new WoFsSub
                            {
                                WoFsDetailId = (int)id,
                                SubPartId = wofsdetailrequest.SubPartIdLst[j]
                            };
                            _repWoFsSub.Add(wofssub);
                        }
                    }
                }
                else if (wofsdetailrequest.Action == "Edit")
                {
                    woFsDetail.Id = wofsdetailrequest.Id;
                    _repWoFsDetail.UpdateWoFsDetail(woFsDetail);

                    var wofssublist = _repWoFsSub.GetWoFsSub(wofsdetailrequest.Id);
                    foreach (var item in wofssublist)
                    {
                        _repWoFsSub.DeleteWoFsSub(item.Id);
                    }

                    if (wofsdetailrequest.SubPartIdLst != null && wofsdetailrequest.SubPartIdLst.Count > 0)
                    {
                        for (var j = 0; j < wofsdetailrequest.SubPartIdLst.Count; j++)
                        {
                            WoFsSub wofssub = new WoFsSub
                            {
                                WoFsDetailId = woFsDetail.Id,
                                SubPartId = wofsdetailrequest.SubPartIdLst[j]
                            };
                            _repWoFsSub.Add(wofssub);
                        }
                    }
                }
            }
        }

        // 转换SPART结构中有用数据给界面使用
        private IList<CommonDTO> GetCommonDTOLst(List<SPart> grplst)
        {
            IList<CommonDTO> comlst = new List<CommonDTO>();
            foreach (var grp in grplst)
            {
                CommonDTO com = new CommonDTO
                {
                    Id = grp.Id,
                    Code = grp.PartNo,
                    Name = grp.PartName
                };
                comlst.Add(com);
            }

            return comlst;
        }

        private IList<WoBomPartDTO> GetWoBomSub(string workorder)
        {
            IList<WoBomPartDTO> wobomlst = new List<WoBomPartDTO>();
            // 获取WO_BOM表中已有替代料
            var wobom = _repWoBom.GetWoBomSubPart(workorder).ToList();
            // 按照相同的SubPartGroup进行分组
            var grouped =
                from item in wobom
                group item
                by new { item.SubPartGroup };

            // IList<SPart> subpartlst = new List<SPart>();
            foreach (var group in grouped)
            {
                var grplst = group.Select(item => item.SSubPart).ToList();
                // 转换结构返回给界面
                IList<CommonDTO> comlst = GetCommonDTOLst(grplst);

                foreach (var items in comlst)
                {
                    // subpartlst.Add(items);
                    WoBomPartDTO woBomPartDTO = new WoBomPartDTO
                    {
                        ItemPart = items,
                        SubPart = comlst
                    };
                    wobomlst.Add(woBomPartDTO);
                }
            }

            return wobomlst;
        }

        public IList<WoBomPartDTO> GetWoBomPart(string workorder)
        {
            IList<WoBomPartDTO> wobomlst;
            // 如果有替代料，找到相同组的替代料
            wobomlst = GetWoBomSub(workorder);
            // 获取没有替代料的WO_BOM数据
            var wobomnosub = _repWoBom.GetWoBomNoSub(workorder).ToList();
            foreach (var item in wobomnosub)
            {
                WoBomPartDTO woBomPartDTO = new WoBomPartDTO
                {
                    ItemPart = new CommonDTO
                    {
                        Id = item.SSubPart.Id,
                        Code = item.SSubPart.PartNo,
                        Name = item.SSubPart.PartName
                    }
                };
                // 有替代料的数据和没有替代料的数据汇总
                wobomlst.Add(woBomPartDTO);
            }

            return wobomlst;
        }

        public void DeleteWoFsDetail(int id)
        {
            var wofssublist = _repWoFsSub.GetWoFsSub(id);
            foreach (var item in wofssublist)
            {
                _repWoFsSub.DeleteWoFsSub(item.Id);
            }

            _commonService.DeleteWoFsDetail(id);
        }
    }
}
