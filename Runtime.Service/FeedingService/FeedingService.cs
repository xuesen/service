using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using IIMes.Infrastructure.Aspect;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Exception;
using IIMes.Infrastructure.Service;
using IIMes.Services.Runtime.Interface;
using IIMes.Services.Runtime.Interface.DTO;
using IIMes.Services.Runtime.Model.Product;
using IIMes.Services.Runtime.Model.System;
using IIMes.Services.Runtime.Model.WOR;
using IIMes.Services.Runtime.Repository;
using IIMes.Services.Runtime.Model.Production;
using System;
using IIMes.Services.Runtime.Model.Process;
using IIMes.Services.Runtime.Model.Plant;

namespace IIMes.Services.Runtime.Services
{
    public class FeedingService : IFeedingService
    {
        private readonly IRepository<FsStatus> _repFsStatus;
        private readonly IRepository<FsStatusDetailLog> _repFsStatusDetailLog;
        private readonly IRepository<FsStatusLog> _repFsStatusLog;
        private readonly IRepository<WoFs> _repWoFs;
        private readonly IRepository<WoFsDetail> _repWoFsDetail;
        private readonly IRepository<TerminalParts> _repTerminalParts;
        private readonly BopRepository _repBop;
        private readonly SpecManager _specmanager;
        private readonly IRepository<Material> _repMaterial;
        private readonly IRepository<Terminal> _repTerminal;

        public FeedingService(
            IRepository<FsStatus> repFsStatus,
            IRepository<FsStatusDetailLog> repFsStatusDetailLog,
            IRepository<FsStatusLog> repFsStatusLog,
            BopRepository bopRepository,
            IRepository<TerminalParts> repTerminalParts,
            IRepository<WoFs> repWoFs,
            IRepository<WoFsDetail> repWoFsDetail,
            IRepository<Material> repMaterial,
            IRepository<Terminal> repTerminal,
            SpecManager specmanager)
        {
            _repFsStatus = repFsStatus;
            _repFsStatusDetailLog = repFsStatusDetailLog;
            _repFsStatusLog = repFsStatusLog;
            _repBop = bopRepository;
            _specmanager = specmanager;
            _repTerminalParts = repTerminalParts;
            _repWoFs = repWoFs;
            _repWoFsDetail = repWoFsDetail;
            _repMaterial = repMaterial;
            _repTerminal = repTerminal;
        }

        // 查询FsStatus
        public object QueryFsStatus(string wo, int terminalId)
        {
            var fsStatus = _repFsStatus.GetFsStatus(wo, terminalId);
            return fsStatus;
        }

        [Transactional]
        public object UpdateFsStatus(FeedingRequestDTO requestDTO)
        {
            var workOrder = requestDTO.WorkOrder;
            var terminalId = requestDTO.TerminalId;
            var status = requestDTO.Status;
            var equipmentId = requestDTO.EquipmentId;
            var editor = requestDTO.Editor;

            // 下料根据equipmentId terminalId更新状态
            if (workOrder == null)
            {
                _repFsStatus.UpdateFsStatusList(equipmentId.Value, terminalId, status);
                return null;
            }

            var fsStatus = _repFsStatus.GetFsStatus(workOrder, terminalId);
            // 存在，更新
            if (fsStatus != null)
            {
                _repFsStatus.UpdateFsStatus(workOrder, terminalId, status);
            }
            // 不存在，新增 
            else
            {
                var x = new FsStatus()
                {
                    TerminalId = terminalId,
                    EquipmentId = equipmentId,
                    WorkOrder = workOrder,
                    Editor = editor,
                    Status = status
                };
                _repFsStatus.Add(x);
            }

            return null;
        }

        // 质检确认上料
        [Transactional]
        public object ConfirmFeeding(FeedingRequestDTO requestDTO)
        {
            var terminalId = requestDTO.TerminalId;
            var equipmentId = requestDTO.EquipmentId;
            var workOrder = requestDTO.WorkOrder;
            var status = requestDTO.Status;
            var editor = requestDTO.Editor;

            // 插入或更新FS_STATUS
            var fsStatus = new FsStatus();
            fsStatus.TerminalId = terminalId;
            fsStatus.EquipmentId = equipmentId;
            fsStatus.WorkOrder = workOrder;
            fsStatus.Status = status;
            fsStatus.Editor = editor;
            AddFsStatus(fsStatus);

            // 插入FS_STATUS_LOG
            var fsStatusLog = new FsStatusLog();
            fsStatusLog.TerminalId = terminalId;
            fsStatusLog.EquipmentId = equipmentId;
            fsStatusLog.WorkOrder = workOrder;
            fsStatusLog.Status = status;
            fsStatusLog.Editor = editor;
            fsStatusLog.Cdt = DateTime.Now;
            var fsStatusLogId = AddFsStatusLog(fsStatusLog);

            // 获取Spec
            var specType = "EquipmentMaterialPreparingCollection";
            var bop = _repBop.GetBopByWoAndEq(workOrder, equipmentId.Value);
            var spec = _specmanager.GetCollectionSpec(specType, terminalId, bop);
            foreach (var collection in spec.CollectionSpecItems)
            {
                foreach (TerminalParts value in collection.Values)
                {
                    // 插入FS_STATUS_DETAIL_LOG
                    var fsStatusDetailLog = new FsStatusDetailLog();
                    fsStatusDetailLog.FsStatusLogId = (int?)fsStatusLogId;
                    fsStatusDetailLog.Station = value.Station;
                    fsStatusDetailLog.ItemPartId = value.ItemPartId;
                    fsStatusDetailLog.ScanPartId = value.ScanPartId;
                    fsStatusDetailLog.ScanNo = value.ScanNo;
                    fsStatusDetailLog.Editor = editor;
                    fsStatusDetailLog.Cdt = DateTime.Now;
                    AddFsStatusDetailLog(fsStatusDetailLog);
                }
            }

            return null;
        }

        private void AddFsStatus(FsStatus fsStatus)
        {
            var x = _repFsStatus.Query()
                .Where(p => p.WorkOrder == fsStatus.WorkOrder && p.TerminalId == fsStatus.TerminalId).FirstOrDefault();
            if (x != null)
            {
                // 存在更新
                x.Status = fsStatus.Status;
                x.Udt = DateTime.Now;
                _repFsStatus.Update(x);
            }
            else
            {
                // 不存在新增
                _repFsStatus.Add(fsStatus);
            }
            return;
        }

        private object AddFsStatusLog(FsStatusLog fsStatusLog)
        {
            var id = _repFsStatusLog.Add(fsStatusLog);
            return id;
        }

        private object AddFsStatusDetailLog(FsStatusDetailLog fsStatusDetailLog)
        {
            var id = _repFsStatusDetailLog.Add(fsStatusDetailLog);
            return id;
        }

        // SMT上料检查
        public bool Check(FeedingRequestDTO requestDTO)
        {
            // 检查WoFs和TerminalPart
            var equipmentId = requestDTO.EquipmentId;
            var workOrder = requestDTO.WorkOrder;
            var terminalId = requestDTO.TerminalId;

            var wofs = _repWoFs.GetWoFsByWoAndEq(workOrder, equipmentId.Value).OrderByDescending(p => p.Id).FirstOrDefault();
            if (wofs == null)
            {
                // 工单料站数据未维护
                throw new BizException("CHK028");
            }

            var woFsId = wofs.Id.ToString();
            var woFsDetails = _repWoFsDetail.Query().Where(p => p.WoFsId == woFsId).ToList();
            var terminalParts = _repTerminalParts.Query().Where(p => p.EquipmentId == equipmentId).ToList();

            // woFsDetails 是否包含 terminalParts
            var result = terminalParts.All(t => woFsDetails.Any(w => w.Station == t.Station && w.ItemPartId == t.ItemPartId));
            if (!result)
            {
                // 设备上物料不符合工单使用
                return false;
            }

            return true;
        }

        [Transactional]
        public object UnfeedingForBomShow(FeedingRequestDTO requestDTO)
        {
            var equipmentId = requestDTO.EquipmentId.Value;

            var parameters = new Dictionary<string, object>();
            parameters["equipmentId"] = equipmentId;
            var list = _repTerminalParts.ExecNamedSqlQuery<TerminalPartsBySMT>("GetTerminalPartInfoForBom", parameters.ToArray()).ToList();

            foreach (var item in list)
            {
                // 如果ScanNo不为空,根据ScanNo查询Material;否则,根据ScanPartNo查询
                var material = !String.IsNullOrEmpty(item.ScanNo) ? _repMaterial.GetPartByReel(item.ScanNo) : _repMaterial.GetPartByPart(item.ScanPartNo);
                var qty = material != null ? material.Amount : 0;
                var LeftAmount = material != null ? material.LeftAmount : 0;
                item.Qty = qty;
                item.LeftAmount = LeftAmount;
            }

            return list;
        }

        [Transactional]
        public object UnfeedingForSMTShow(FeedingRequestDTO requestDTO)
        {
            var equipmentId = requestDTO.EquipmentId.Value;

            var parameters = new Dictionary<string, object>();
            parameters["equipmentId"] = equipmentId;
            var list = _repTerminalParts.ExecNamedSqlQuery<TerminalPartsBySMT>("GetTerminalPartInfoForSMT", parameters.ToArray()).ToList();

            list.ForEach(p => p.FeederType = GetFeederType(p));
            list.ForEach(p => p.Location = GetLocation(p));
            list.ForEach(p => p.ItemCount = GetItemCount(p));

            /* SMT上料细节 不需要了
            // WO_FS_DETAL.ID == TERMINAL_PARTS.STATION_ID
            foreach (var item in list)
            {
                var values = _repTerminalParts.Query().Where(p => p.StationId == item.StationId);
                var ItemValues = (from value in values
                                  let itemValue = new TerminalPartsByBom
                                  {
                                      Id = value.Id,
                                      // ItemId = value.ItemId,
                                      CandidateId = value.ScanPartId,
                                      Value = value.ScanNo,
                                      Sequence = value.Sequence,
                                      ScanPartNo = value.ScanPartNo,
                                      Station = value.Station,
                                      ItemPartNo = value.ItemPartNo,
                                      Amount = _repMaterial.GetPartByReel(value.ScanNo).Amount,
                                      LeftAmount = _repMaterial.GetPartByReel(value.ScanNo).LeftAmount
                                  }
                                  select itemValue).ToList();

                item.Values = ItemValues;
            }
            */

            return list;
        }

        private string GetFeederType(TerminalPartsBySMT item)
        {
            var woFsDetail = _repWoFsDetail.Get(item.ItemId);
            var feederType = woFsDetail != null ? woFsDetail.FeederType : null;
            return feederType;
        }

        private string GetLocation(TerminalPartsBySMT item)
        {
            var woFsDetail = _repWoFsDetail.Get(item.ItemId);
            var location = woFsDetail != null ? woFsDetail.Location : null;
            return location;
        }

        private int GetItemCount(TerminalPartsBySMT item)
        {
            var woFsDetail = _repWoFsDetail.Get(item.ItemId);
            var itemCount = woFsDetail != null ? woFsDetail.ItemCount : 0;
            return itemCount;
        }

        [Transactional]
        public void Unfeeding(TerminalPartsBySMT requestDTO)
        {
            var terminalId = requestDTO.TerminalId;
            var equipmentId = requestDTO.EquipmentId;
            var station = requestDTO.Station;
            var itemPartNo = requestDTO.ItemPartNo;

            if (station != null)
            {
                _repTerminalParts.DeleteByLambda(p => p.TerminalId == terminalId && p.EquipmentId == equipmentId && p.Station == station);
                return;
            }
            else if (itemPartNo != null)
            {
                _repTerminalParts.DeleteByLambda(p => p.TerminalId == terminalId && p.EquipmentId == equipmentId && p.ItemPartNo == itemPartNo);
                return;
            }
            else
            {
                _repTerminalParts.DeleteByLambda(p => p.TerminalId == terminalId && p.EquipmentId == equipmentId);
                return;
            }
        }

        public bool CheckWoBom(FeedingRequestDTO requestDTO)
        {
            // 检查WoBom和TerminalPart
            var workOrder = requestDTO.WorkOrder;
            var terminalId = requestDTO.TerminalId;

            var bop = _repBop.GetBop(workOrder);
            var terminal = _repTerminal.Get(terminalId);
            var processBopBom = bop.BopBom.Where(o => o.ProcessId == terminal.Process.Id);

            var processWoBom = bop.WoBom.Where(o => o.ProcessId == terminal.Process.Id);

            var woBomParts = new List<int>();
            foreach (var bopBomItem in processBopBom)
            {
                var woBomItem = processWoBom.Where(o => o.SubPart.Id == bopBomItem.ItemPartId).FirstOrDefault();
                var parts = processWoBom.Where(o => o.SubPartGroup == woBomItem.SubPartGroup).Select(o => o.SubPart).ToList();
                var list = parts.Select(p => p.Id).ToList();
                list.Add(bopBomItem.ItemPartId);
                woBomParts.AddRange(list);
            }

            woBomParts = woBomParts.Distinct().ToList();
            // 绑定的任意料品与当前工单中获取的BOM不匹配时，提示：当前Terminal已上料品与当前工单不一致;
            var terminalParts = _repTerminalParts.Query().Where(p => p.TerminalId == terminalId).ToList();
            var itemPartIds = terminalParts.Select(x => x.ItemPartId);

            // WoBom 是否包含 terminalParts
            woBomParts = woBomParts.Distinct().ToList();
            itemPartIds = itemPartIds.Distinct().ToList();
            if(itemPartIds.All(t => woBomParts.Any(w => w == t)))
            {
                return true;
            }
            else if (itemPartIds.Count() == 0)
            {
                return true;
            }

            return false;
        }
    }
}