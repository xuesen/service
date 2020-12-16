using System;
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
using IIMes.Services.Maintain.Model;
using IIMes.Services.Maintain.Repository;

namespace IIMes.Services.Maintain.Services
{
    public partial class WoService : BaseMaintainService<WoBase, WoDTO>, IWoService
    {
        private readonly IRepository<WoBase> _repWobase;
        private readonly IRepository<SPart> _repPart;
        private readonly IRepository<WoStatusLog> _repWoStatusLog;
        private readonly IRepository<SWorkflowBase> _repWorkflowBase;
        private readonly IRepository<SWorkflow> _repWorkflow;
        private readonly IRepository<SBop> _repBop;
        private readonly IRepository<SBopBase> _repBopBase;
        private readonly IRepository<SBomBase> _repBomBase;
        private readonly IRepository<SBom> _repBom;
        private readonly IRepository<SBopProcessBom> _repBomProcessBom;
        private readonly IRepository<WoBom> _repWoBom;
        private readonly IRepository<SBomItem> _repBomItem;
        private readonly IRepository<SWoStatus> _repWoStatus;
        private readonly IRepository<SBarcodeRule> _repBarcodeRule;
        private readonly IRepository<SProductLine> _repProductLine;

        public WoService(
                            IMapper mapper,
                            IRepository<WoBase> repWobase,
                            IRepository<SPart> repPart,
                            IRepository<WoStatusLog> repWoStatusLog,
                            IRepository<SWorkflowBase> repWorkflowBase,
                            IRepository<SWorkflow> repWorkflow,
                            IRepository<SBop> repBop,
                            IRepository<SBopBase> repBopBase,
                            IRepository<SBomBase> repBomBase,
                            IRepository<SBom> repBom,
                            IRepository<SBopProcessBom> repBomProcessBom,
                            IRepository<WoBom> repWoBom,
                            IRepository<SBomItem> repBomItem,
                            IRepository<SWoStatus> repWoStatus,
                            IRepository<SBarcodeRule> repBarcodeRule,
                            IRepository<SProductLine> repProductLine)
        : base(repWobase, mapper)
        {
            _repWobase = repWobase;
            _repPart = repPart;
            _repWoStatusLog = repWoStatusLog;
            _repWorkflowBase = repWorkflowBase;
            _repWorkflow = repWorkflow;
            _repBop = repBop;
            _repBopBase = repBopBase;
            _repBomBase = repBomBase;
            _repBom = repBom;
            _repBomProcessBom = repBomProcessBom;
            _repWoBom = repWoBom;
            _repBomItem = repBomItem;
            _repWoStatus = repWoStatus;
            _repBarcodeRule = repBarcodeRule;
            _repProductLine = repProductLine;
        }

        protected void CheckDuplication(string wo)
        {
            var bizKeyName = "WorkOrder";
            var t = typeof(WoBase);
            var keyProperty = t.GetProperty(bizKeyName);
            if (keyProperty != null)
            {
                var duplications = _repWobase.Query(new QueryParameter() { Name = bizKeyName, Value = wo, Fuzzy = false });
                if (duplications != null && duplications.Count > 0)
                {
                    throw new BizException("MTN001", wo);
                }
            }
        }

        public IList<WoDTO> Query(WoRequestDTO worequest)
        {
            IList<WoInfoDTO> woinfolst = new List<WoInfoDTO>();
            IList<WoDTO> wodtolst = new List<WoDTO>();
            var partobj = _repPart.GetPart(worequest.PartNo);
            var wostatusobj = _repWoStatus.GetWoStausByCode(worequest.WoStatus);
            var pdlineobj = _repProductLine.GetPdLineByCode(worequest.PdLineCode);
            var partid = "";
            var wostatusid = "";
            var pdlineid = "";
            if (partobj != null)
            {
                partid = partobj.Id.ToString();
            }

            if (wostatusobj != null)
            {
                wostatusid = wostatusobj.Id.ToString();
            }

            if (pdlineobj != null)
            {
                pdlineid = pdlineobj.Id.ToString();
            }

            var parameters = new Dictionary<string, object>
                                {
                                    { "wo", worequest.WorkOrder },
                                    { "wostatusid", wostatusid },
                                    { "partid",  partid },
                                    { "pdlineid", pdlineid },
                                    { "scheduletime", worequest.WoScheduleTime }
                                };
            woinfolst = _repWobase.ExecNamedSqlQuery<WoInfoDTO>("QueryWo", parameters.ToArray());
            // var wobaselst = _repWobase.QueryWos(worequest.WorkOrder, worequest.WoStatus, worequest.PartNo, worequest.WoScheduleTime, worequest.PdLineCode).ToList();

            foreach (var item in woinfolst)
            {
                LayerEditorDTO editordto = new LayerEditorDTO
                {
                    Name = item.Editor,
                    Code = item.Editor
                };
                var po = "";
                if (item.Po != null)
                {
                    po = item.Po;
                }

                var desc = "";
                if (item.Description != null)
                {
                    desc = item.Description;
                }

                var snruleid = -1;
                var snrulename = "";
                if ((int)item.SnBarcodeRuleId != -1)
                {
                    var snrule = _repBarcodeRule.Get((int)item.SnBarcodeRuleId);
                    snruleid = snrule.Id;
                    snrulename = snrule.RuleName;
                }

                if (item.Description != null)
                {
                    desc = item.Description;
                }

                var part = _repPart.Get(item.PartId);
                var wostatus = _repWoStatus.Get(item.WoStatusId);
                var pdline = _repProductLine.Get(item.PdLineId);
                WoDTO wodto = new WoDTO
                {
                    Id = item.Id,
                    WorkOrder = item.WorkOrder,
                    TargetQty = (int)item.TargetQty,
                    PartId = item.PartId,
                    PartNo = part.PartNo,
                    PartName = part.PartName,
                    WoStatusId = item.WoStatusId,
                    WoStatusCode = wostatus.Code,
                    WoStatusName = wostatus.Name,
                    PdLineId = item.PdLineId,
                    PdLineCode = pdline.Code,
                    PdLineName = pdline.Name,
                    Sequence = (int)item.Sequence,
                    InputQty = (int)item.InputQty,
                    OutPutQty = (int)item.OutputQty,
                    WoScheduleTime = string.Format("{0:d}", item.WoScheduleTime),
                    WoScheduleFinishTime = string.Format("{0:d}", item.WoScheduleFinishTime),
                    SnBarcodeRuleId = snruleid,
                    SnBarcodeRule = snrulename,
                    // RcBarcodeRuleId = item.RcBarcodeRule.Id,
                    // RcBarcodeRule = item.RcBarcodeRule.RuleName,
                    WoCreateTime = string.Format("{0:d}", item.WoCreateTime),
                    WoUpdateTime = string.Format("{0:d}", item.WoUpdateTime),
                    Po = po,
                    Description = desc,
                    Type = item.Type,
                    Update_time = item.UpdateTime.ToString(),
                    Editor = editordto
                };
                wodtolst.Add(wodto);
            }

            return wodtolst;
        }

        [Transactional]
        public int AddWo(WoBaseRequestDTO woBaseRequest)
        {
            var ret = 0;
            if (woBaseRequest != null)
            {
                dynamic snBarcodeRuleId = null;
                dynamic rcBarcodeRuleId = null;
                if (woBaseRequest.SnBarcodeRuleId != null)
                {
                    snBarcodeRuleId = woBaseRequest.SnBarcodeRuleId;
                }

                if (woBaseRequest.RcBarcodeRuleId != -1)
                {
                    rcBarcodeRuleId = woBaseRequest.RcBarcodeRuleId;
                }

                if (woBaseRequest.Modify == false)
                {
                    CheckDuplication(woBaseRequest.WorkOrder);
                    var maxSquence = 0;
                    var wobase = _repWobase.Query().OrderByDescending(o => o.Sequence).FirstOrDefault();
                    if (wobase != null)
                    {
                        maxSquence = (int)wobase.Sequence;
                    }

                    var part = _repPart.Get(woBaseRequest.PartId);
                    var wostatus = _repWoStatus.GetWoStaus("INITIAL");
                    WoBase woBase = new WoBase
                    {
                        WorkOrder = woBaseRequest.WorkOrder,
                        ProductLineId = woBaseRequest.PdLineId,
                        SPart = part,
                        Type = woBaseRequest.Type,
                        TargetQty = woBaseRequest.TargetQty,
                        WoCreateTime = DateTime.Now,
                        WoScheduleTime = woBaseRequest.WoScheduleTime,
                        WoScheduleFinishTime = woBaseRequest.WoScheduleFinishTime,
                        // WoStartTime = woBaseRequest.WoStartTime,
                        // WoCloseTime = woBaseRequest.WoCloseTime,
                        InputQty = woBaseRequest.InputQty,
                        OutputQty = woBaseRequest.OutputQty,
                        StatusId = wostatus.Id,
                        SnBarcodeRuleId = snBarcodeRuleId,
                        RcBarcodeRuleId = rcBarcodeRuleId,
                        Sequence = maxSquence + 1,
                        Po = woBaseRequest.Po,
                        Description = woBaseRequest.Description,
                        Editor = woBaseRequest.Editor
                    };
                    _repWobase.Add(woBase);
                }
                else
                {
                    var wobaseobj = _repWobase.GetWo(woBaseRequest.WorkOrder);
                    wobaseobj.ProductLineId = woBaseRequest.PdLineId;
                    wobaseobj.SnBarcodeRuleId = snBarcodeRuleId;
                    wobaseobj.RcBarcodeRuleId = rcBarcodeRuleId;
                    wobaseobj.TargetQty = woBaseRequest.TargetQty;
                    wobaseobj.WoScheduleTime = woBaseRequest.WoScheduleTime;
                    wobaseobj.WoScheduleFinishTime = woBaseRequest.WoScheduleFinishTime;
                    wobaseobj.Po = woBaseRequest.Po;
                    wobaseobj.Description = woBaseRequest.Description;
                    wobaseobj.Type = woBaseRequest.Type;
                }
            }

            return ret;
        }

        [Transactional]
        public int SaveWo(WoBaseRequestDTO woBaseRequest)
        {
            var ret = 0;
            if (woBaseRequest != null)
            {
                dynamic snBarcodeRuleId = null;
                dynamic rcBarcodeRuleId = null;
                if (woBaseRequest.SnBarcodeRuleId != -1)
                {
                    snBarcodeRuleId = woBaseRequest.SnBarcodeRuleId;
                }

                if (woBaseRequest.RcBarcodeRuleId != -1)
                {
                    rcBarcodeRuleId = woBaseRequest.RcBarcodeRuleId;
                }

                if (woBaseRequest.Modify == false)
                {
                    CheckDuplication(woBaseRequest.WorkOrder);
                    var maxSquence = 0;
                    var wobase = _repWobase.Query().OrderByDescending(o => o.Sequence).FirstOrDefault();
                    if (wobase != null)
                    {
                        maxSquence = (int)wobase.Sequence;
                    }

                    var part = _repPart.Get(woBaseRequest.PartId);
                    var wostatus = _repWoStatus.GetWoStaus("INITIAL");
                    WoBase woBase = new WoBase
                    {
                        WorkOrder = woBaseRequest.WorkOrder,
                        ProductLineId = woBaseRequest.PdLineId,
                        SPart = part,
                        Type = woBaseRequest.Type,
                        TargetQty = woBaseRequest.TargetQty,
                        WoCreateTime = DateTime.Now,
                        WoScheduleTime = woBaseRequest.WoScheduleTime,
                        WoScheduleFinishTime = woBaseRequest.WoScheduleFinishTime,
                        // WoStartTime = woBaseRequest.WoStartTime,
                        // WoCloseTime = woBaseRequest.WoCloseTime,
                        InputQty = woBaseRequest.InputQty,
                        OutputQty = woBaseRequest.OutputQty,
                        StatusId = wostatus.Id,
                        SnBarcodeRuleId = snBarcodeRuleId,
                        RcBarcodeRuleId = rcBarcodeRuleId,
                        Sequence = maxSquence + 1,
                        Po = woBaseRequest.Po,
                        Description = woBaseRequest.Description,
                        Editor = woBaseRequest.Editor
                    };
                    _repWobase.Add(woBase);
                }
                else
                {
                    var wobaseobj = _repWobase.GetWo(woBaseRequest.WorkOrder);
                    wobaseobj.ProductLineId = woBaseRequest.PdLineId;
                    wobaseobj.SnBarcodeRuleId = snBarcodeRuleId;
                    wobaseobj.RcBarcodeRuleId = rcBarcodeRuleId;
                    wobaseobj.TargetQty = woBaseRequest.TargetQty;
                    wobaseobj.WoScheduleTime = woBaseRequest.WoScheduleTime;
                    wobaseobj.WoScheduleFinishTime = woBaseRequest.WoScheduleFinishTime;
                    wobaseobj.Po = woBaseRequest.Po;
                    wobaseobj.Description = woBaseRequest.Description;
                    wobaseobj.Type = woBaseRequest.Type;
                    // modify ITC-1755-0114
                    wobaseobj.Udt = DateTime.Now;
                }
            }

            return ret;
        }

        [Transactional]
        public void UpdateWoStatus(WoStatusRequestDTO woRequest)
        {
            if (woRequest != null)
            {
                var wobase = _repWobase.GetWo(woRequest.WorkOrder);

                if (woRequest.WoStatusName == "RELEASE")
                {
                      SyncData(wobase, woRequest.Editor);
                }

                wobase.StatusId = woRequest.WoStatusId;
                wobase.Udt = DateTime.Now;
                WoStatusLog woStatusLog = new WoStatusLog
                {
                    WoBaseId = wobase.Id,
                    StatusId = woRequest.WoStatusId,
                    Reason = woRequest.Reason,
                    Editor = woRequest.Editor
                };
                _repWoStatusLog.Add(woStatusLog);
            }
        }

        private void SyncData(WoBase wobase, string editor)
        {
            // 工单RELEASE时，自动填入BOM版本（最新版）
            // 记录工单的工艺路线与工艺清单
            // （有机型工艺路线自动带机型的，没有机型的自动带产品系列的，使用最新版ID，工艺清单使用工艺路线对应的最新发布版本ID）。
            var part = _repPart.Get(wobase.SPart.Id);
            var workflowbase = _repWorkflowBase.GetWorkflowByPartId(part.Id);
            if (workflowbase == null)
            {
                workflowbase = _repWorkflowBase.GetWorkflowByFamilyId(part.SFamily.Id);
            }

            if (workflowbase == null)
            {
                throw new BizException("CHK027");
            }

            var workflow = _repWorkflow.GetWorkflow(workflowbase.Id, workflowbase.LastVersion);
            wobase.SWorkflow = workflow;
            var bop = _repBop.GetBop(workflow.Id);
            if (bop == null)
            {
                throw new BizException("CHK024");
            }

            wobase.BopId = bop.Id;
            var bombase = _repBomBase.GetBomBase(wobase.SPart.Id);
            // modify ITC-1755-0126
            if (bombase == null)
            {
                throw new BizException("CHK036");
            }

            var bom = _repBom.GetBom(bombase.Id, bombase.LastVersion);
            wobase.BomId = bom.Id;
            SaveBomInfo(wobase, bop, bom, editor);
        }

        private void SaveBomInfo(WoBase woBase, SBop bop, SBom bom, string editor)
        {
            // 把机型S_BOM根据工艺清单配置的收料，写入WO_BOM。
            // S_BOM.ITEM_PART_ID =S_BOP_PROCESS_BOM. ITEM_PART_ID，把S_BOM_ITEM同ITEM_GROUP都带到工单BOM。
            var bopbase = _repBopBase.GetBopBase(bop.WorkflowId);
            var bopprocessbomlst = _repBomProcessBom.GetBomProcessBom(bopbase.Id).ToList();
            if (bopprocessbomlst.Count == 0)
            {
                // 工艺清单未维护物料清单时不向WO_BOM中导入数据
                return;
                // throw new BizException("CHK025");
            }

            foreach (var item in bopprocessbomlst)
            {
                // modify ITC-1755-0151
                var bomitem = _repBomItem.GetBomItem(item.ItemPart.Id, bom.Id);
                if (bomitem.ItemGroup == null || bomitem.ItemGroup == "")
                {
                    continue;
                }

                var bomitemlst = _repBomItem.GetBomItemByGroup(bomitem.ItemGroup, bom.Id).ToList();
                foreach (var items in bomitemlst)
                {
                    WoBom wobom = new WoBom
                    {
                        WoBaseId = woBase.Id,
                        ProcessId = item.Process.Id,
                        MainPartId = woBase.SPart.Id,
                        SubPartId = items.SPart.Id,
                        SubPartGroup = items.ItemGroup,
                        SubPartQty = (int)items.ItemCount,
                        Version = items.ItemVersion,
                        Location = item.Location,
                        Editor = editor
                    };
                    _repWoBom.Add(wobom);
                }
            }
        }

        [Transactional]
        public void SortWo(WoSortRequestDTO woRequest)
        {
            if (woRequest != null)
            {
                var resfrom = _repWobase.Get(woRequest.FromId);
                var resto = _repWobase.Get(woRequest.ToId);
                var fromSequence = resfrom.Sequence;
                var toSequence = resto.Sequence;
                resfrom.Sequence = toSequence;
                resto.Sequence = fromSequence;
            }
        }
    }
}
