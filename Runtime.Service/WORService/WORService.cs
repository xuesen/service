using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using IIMes.Infrastructure.Aspect;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Exception;
using IIMes.Infrastructure.Service;
using IIMes.Services.Runtime.Interface;
using IIMes.Services.Runtime.Interface.DTO;
using IIMes.Services.Runtime.Model.Plant;
using IIMes.Services.Runtime.Model.Process;
using IIMes.Services.Runtime.Model.Product;
using IIMes.Services.Runtime.Model.Production;
using IIMes.Services.Runtime.Model.System;
using IIMes.Services.Runtime.Model.WOR;
using IIMes.Services.Runtime.Repository;

namespace IIMes.Services.Runtime.Services
{
    public class WORService : IWORService
    {
        private readonly SpecManager _specmanager;
        private readonly BopRepository _repBop;
        private readonly IRepository<WorStatus> _repWorStatus;
        private readonly IRepository<WoBase> _repWobase;
        private readonly IRepository<WoStatus> _repWoStatus;
        private readonly IRepository<Terminal> _repTerminal;
        private readonly IRepository<WorkflowStep> _repWorkflowStep;
        private readonly IRepository<Process> _repProcess;
        private readonly IRepository<ProcessType> _repProcessType;
        private readonly IRepository<WoFs> _repWoFs;
        private readonly IRepository<FsStatus> _repFsStatus;
        private readonly IRepository<Setting> _repSetting;
        private readonly IRepository<TerminalParts> _repTerminalParts;
        private readonly IRepository<WorLog> _repWorLog;
        private readonly IRepository<Material> _repMaterial;
        private readonly IRepository<Part> _repPart;
        private readonly IRepository<WorInfo> _repworInfo;
        private readonly IRepository<WorDefect> _repworDefect;
        private readonly IRepository<WorRepairLog> _repworRepairLog;
        private readonly IRepository<WorRepairLogDetail> _repworRepairLogDetail;

        public WORService(SpecManager specmanager,
                          BopRepository bopRepository,
                          IRepository<WorStatus> repWorStatus,
                          IRepository<WoBase> repWoBase,
                          IRepository<WoStatus> repWoStatus,
                          IRepository<Terminal> repTerminal,
                          IRepository<WorkflowStep> repWorkflowStep,
                          IRepository<Process> repProcess,
                          IRepository<ProcessType> repProcessType,
                          IRepository<WoFs> repWoFs,
                          IRepository<FsStatus> repFsStatus,
                          IRepository<Setting> repSetting,
                          IRepository<TerminalParts> repTerminalParts,
                          IRepository<WorLog> repWorLog,
                          IRepository<Material> repMaterial,
                          IRepository<Part> repPart,
                          IRepository<WorInfo> repworInfo,
                          IRepository<WorDefect> repworDefect,
                          IRepository<WorRepairLog> repworRepairLog,
                          IRepository<WorRepairLogDetail> repworRepairLogDetail)
        {
            _specmanager = specmanager;
            _repBop = bopRepository;
            _repWorStatus = repWorStatus;
            _repWobase = repWoBase;
            _repWoStatus = repWoStatus;
            _repTerminal = repTerminal;
            _repWorkflowStep = repWorkflowStep;
            _repProcess = repProcess;
            _repProcessType = repProcessType;
            _repWoFs = repWoFs;
            _repFsStatus = repFsStatus;
            _repSetting = repSetting;
            _repTerminalParts = repTerminalParts;
            _repWorLog = repWorLog;
            _repMaterial = repMaterial;
            _repPart = repPart;
            _repworInfo = repworInfo;
            _repworDefect = repworDefect;
            _repworRepairLog = repworRepairLog;
            _repworRepairLogDetail = repworRepairLogDetail;
        }

        // 开工，更新工单状态
        [Transactional]
        public bool SaveWos(string workorder)
        {
            var ret = false;
            // 开工时，如果该工单的首站是SMT段的，需要检查是否做过上料
            // 暂时不做这部分检查，后续再加 2020.10.15
            // CheckSMTProcess(workorder);
            var statusid = _repWoStatus.GetStatusidByName("WIP").Id;
            // 开工，更新工单状态为WIP
            _repWobase.UpdateWobaseStatusBywo(workorder, statusid);
            ret = true;
            return ret;
        }

        private void CheckSMTProcess(string workorder)
        {
            var workflowid = GetWorkFlowByWo(workorder).Id;
            var workflowstep = _repWorkflowStep.GetWorkFlowStep(workflowid).ToList()[0];
            if (workflowstep.NextProcess.Stage.Code == "SMT")
            {              
                var wofslist = _repWoFs.GetWoFs(workorder).ToList();
                // 检查WO_FS是否存在数据，不存在报错：请维护工单料站
                if (wofslist.Count == 0)
                {
                    throw new BizException("CHK019");
                }
                else
                {
                    var terminal = _repTerminal.GetTerminalByProcessCode(workflowstep.NextProcess.Code);
                    var fsstatus = _repFsStatus.GetFsStatus(workorder, terminal.Id);
                    // WO_FS存在数据，但FS_STATUS.Status不等于“上料已确认”，报错：该工单贴片机尚未完成上料，请先完成上料。
                    if (fsstatus == null)
                    {
                        throw new BizException("CHK031");
                    }
                    if(fsstatus.Status != 1)
                    {
                        throw new BizException("CHK020");
                    }
                }
            }            
        }

        // 报工，进站检查
        // 检查当前站为SMT IN（SMT段首站）进行检查
        public bool CheckSMTFirstProcess(WorRequestDTO request)
        {
            var ret = false;
            // 判断当前工序为SMT贴片机工序
            var process = GetProcessByTerminal(request.TerminalCode);
            if (CheckFirstProcess(request.WorkOrder, request.TerminalCode))
            {
                ret = true;
                /*
                暂时不做这部分检查，后续再加 2020.10.15

                if (process.Stage.Code == "SMT")
                {              
                    var wofslist = _repWoFs.GetWoFs(request.WorkOrder).ToList();
                    // 检查WO_FS是否存在数据，不存在报错：请维护工单料站
                    if (wofslist.Count == 0)
                    {
                        throw new BizException("CHK019");
                    }
                    else
                    {
                        // modify ITC-1755-0082
                        var terminal = _repTerminal.GetTerminal(request.TerminalCode);
                        var fsstatus = _repFsStatus.GetFsStatus(request.WorkOrder, terminal.Id);
                        // WO_FS存在数据，但FS_STATUS.Status不等于“上料已确认”，报错：该工单贴片机尚未完成上料，请先完成上料。
                        if (fsstatus == null)
                        {
                            throw new BizException("CHK031");
                        }
                        if(fsstatus.Status != 1)
                        {
                            throw new BizException("CHK020");
                        }
                    }
                }
                */
            }
            else
            {
                var worstatus = _repWorStatus.GetWorStatusByWo(request.WorkOrder);
                if (worstatus == null)
                {
                    throw new BizException("CHK029");
                }
            }

            return ret;
        }
        // 获取途程
        private Workflow GetWorkFlowByWo(string workorder)
        {
            var workflow = _repWobase.GetWoBaseByWo(workorder).Workflow;
            if (workflow == null)
            {
                throw new BizException("CHK021");
            }
            return workflow;
        }
        // 获取工序
        private Process GetProcessByTerminal(string terminalcode)
        {
            var process = _repTerminal.GetTerminal(terminalcode).Process;
            if (process == null)
            {
                throw new BizException("CHK022");
            }
            return process;
        }
        // 判断当前工序是否为首站
        private bool CheckFirstProcess(string wo, string terminalcode)
        {
            var ret = false;
            var workflowid = GetWorkFlowByWo(wo).Id;
            var processid = GetProcessByTerminal(terminalcode).Id;
            var workflowsteplst = _repWorkflowStep.GetWorkFlowStep(workflowid).ToList();
            var flag = false;
            if (workflowsteplst.Count > 0)
            {             
                foreach (var item in workflowsteplst)
                {       
                    if (item.NextProcess != null)
                    {
                        if (item.NextProcess.Id == processid)
                        {
                            flag = true;
                        }
                    }          
                }
            }

            // modify ITC-1755-0157
            if (flag == false)
            {
                throw new BizException("CHK038");
            }

            var workflowstep = _repWorkflowStep.GetWorkFlowStepByNextProcessId(workflowid, processid);
            if (workflowstep.Process == null)
            {
                ret = true;
            }
            return ret;
        }
        // 判断当前工序是否为最后工序
        private int CheckLastProcessByWo(string wo, string terminalcode)
        {
            var ret = -1;
            var workflowid = GetWorkFlowByWo(wo).Id;
            var processid = GetProcessByTerminal(terminalcode).Id;
            var workflowstep = _repWorkflowStep.GetWorkFlowStepByProcessId(workflowid, processid);
            if (workflowstep.NextProcess == null)
            {
                ret = processid;
            }
            return ret;
        }

        // 报工，进站检查
        // 检查当前站是否为最后一个工序进行检查
        // modify ITC-1755-0106
        public bool CheckLastProcess(WorRequestDTO request)
        {
            var ret = false;
            if (CheckLastProcessByWo(request.WorkOrder, request.TerminalCode) != -1)
            {
                ret = true;
            }
            // ret = true, 表示是最后一个工序
            return ret;
        }

        // 检查当前站无Fail分支时，输入不良品数量框做灰掉处理
        public bool CheckWorkflowStep(WorRequestDTO request)
        {
            var ret = false;
            var workflowid = GetWorkFlowByWo(request.WorkOrder).Id;
            var processid = GetProcessByTerminal(request.TerminalCode).Id;
            ret = GetFailBatch(workflowid, processid);
            // ret = false, 表示无fail分支
            return ret;
        }

        private bool GetFailBatch(int workflowid, int processid)
        {
            var parameters = new Dictionary<string, object>
                                {
                                    { "workflowId", workflowid },
                                    { "processId", processid }
                                };
            var ret = false;
            var name = _repWorkflowStep.ExecNamedSqlQuery<string>("GetFailBatch", parameters.ToArray());
            if (name.Count == 0)
            {
                return ret;
            }
            if (name[0] == "FAIL")
            {
                ret = true;
            }
            return ret;
        }

        public WorStatusDTO GetWorStatus(WorRequestDTO request)
        {
            var processid = GetProcessByTerminal(request.TerminalCode).Id;
            var worstatus = _repWorStatus.GetWorStatus(request.WorkOrder, processid);
            var passqty = 0;
            var failqty = 0;
            if (worstatus != null)
            {
                passqty = worstatus.PassQty;
                failqty = worstatus.FailQty;
            }
            WorStatusDTO worstatusdto = new WorStatusDTO
            {
                WorkOrder = request.WorkOrder,
                PassQty = passqty,
                FailQty = failqty
            };
            return worstatusdto;
        }

        // 报工
        [Transactional]
        public int SaveWor(WorRequestDTO request)
        {
            var ret = 0;
            // 扣料
            // IsReflow = true, 勾选维修回流板卡报工,不扣料
            // IsReflow = false, 不勾选维修回流板卡报工,扣料
            // request.SN , 质检：不良报工时需要传入刷入的Fail板卡的惟一标识码
            if (request.SN == null || request.SN == "")
            {
                if (request.IsReflow == false)
                {
                    CollectItems(request);
                }
            }
            else
            {
                // CheckWorRepair(request.SN);
                // 质检不良报工，passqty = 0 ; 输入板卡唯一条码，按一块板卡完成报工，failqty = 1
                request.InputPassQty = 0;
                request.InputFailQty = 1;
            }
             
            // 首站报工
            if (request.IsFirstProcess == true)
            {
                SaveFirstProcess(request); 
            }    
                 
            // 良品，不良品报工
            UpdateWorStatus(request);

            // 最后工序报工
            var lastprocessid = CheckLastProcessByWo(request.WorkOrder, request.TerminalCode);
            if ( lastprocessid != -1)
            {
                SaveLastProcess(request, lastprocessid); 
            } 
            // 插入WOR_LOG
            var logid = AddWorLog(request);
            // 质检不良报工，插入WOR_DEFECT
            if (request.SN != null && request.SN != "")
            {
                AddWorDefect(request, logid);
            }
            return ret;
        }

        private void CollectItems(WorRequestDTO request)
        {            

            // 通过terminalcode获取processcode -> processtype, processtype维护在SSetting表中
            // SSetting表中program = processtype, code = ChipMount 为SMT贴片机
            var checktype = CheckProcessType(request.TerminalCode);
            var terminal = _repTerminal.GetTerminal(request.TerminalCode);

            // 如果是SMT贴片机
            // 贴片机：报工数量* WO_FS_DETAIL.ITEM_COUNT，先上的REELID先扣
            // 
            // 非贴片机
            // 非贴片机：报工数量* S_BOP_PROCESS_BOM.QTY            
            var spec = GetSpecByType(checktype, terminal.Id, request.WorkOrder);
            CollectionSpec(request, spec);
        }

        private ICollectionSpec GetSpecByType(bool checktype, int terminalId, string wo)
        {
            string specType;
            if (checktype == true)
            {
                specType = "Equipment";
            }
            else
            {
                specType = "Bom";
            }
            var bop = _repBop.GetBopByWo(wo);
            var spec = _specmanager.GetCollectionSpec(specType, terminalId, bop);
            return spec;
        }

        private void CollectionSpec(WorRequestDTO request, ICollectionSpec spec)
        {
            // modify ITC-1755-0164
            if (spec.CollectionSpecItems.Count == 0)
            {
                // 缺料
                // throw new BizException("CHK017", request.TerminalCode);
                // 未在工艺清单中维护物料清单，所以不需要扣料
                return;
            }
            var reel = "";              
            foreach (var item in spec.CollectionSpecItems)
            {
                if (item.Values.Count == 0)
                {
                    // 缺料
                    throw new BizException("CHK017", request.TerminalCode);
                }  
                // 扣料数量
                var deductqty = item.Qty * request.InputPassQty;
                foreach (var items in item.Values)
                {
                    var material = _repMaterial.GetPartLstByReel(items.Value).ToList()[0];                                       

                    // var part = _repPart.Get(items.CandidateId);
                    // var materiallst = _repMaterial.GetPartLstByPart(part.PartNo).ToList();                                       
                    // if (materiallst.Count == 0)
                    // {
                    //     materiallst = _repMaterial.GetPartLstByReel(items.Value).ToList();
                    // }
                    if (deductqty > 0)
                    {

                        // for (var i = 0; i < materiallst.Count; i++)
                        {   
                            var usedqty = 0;                        
                            if ( deductqty >= items.LeftAmount)
                            {
                                reel = items.Value;
                                deductqty -= items.LeftAmount;
                                material.LeftAmount = 0;
                                usedqty = items.LeftAmount;
                                // 扣完时，从TERMINAL_PARTS删除此ReelID
                                DeleteTerminalParts(items.CandidateId);
                            }
                            else
                            {
                                usedqty = deductqty;
                                material.LeftAmount -= deductqty;
                                deductqty = 0;                              
                            } 
                            // 用料信息插入到WOR_INFO
                            if (usedqty != 0)
                            {
                                AddWorInfo(request, items, usedqty);
                            }                                                                                
                        }

                    }
                }    
                if (deductqty != 0)
                {
                    throw new BizException("CHK018", reel, request.TerminalCode);
                }
                // break;                
            }
        }

        private void DeleteTerminalParts(int CandidateId)
        {
            // _repTerminalParts.DeleteByScanPartId(CandidateId);
        }

        private bool CheckProcessType(string terminalcode)
        {
            // ret == true, 为贴片机工序类型
            var ret = false;
            var terminal = _repTerminal.GetTerminal(terminalcode);
            var program = "ProcessType";
            var setting = _repSetting.GetSettingByProgram(program).ToList();
            if (setting[0].Code == terminal.Process.ProcessType.Code)
            {
                ret = true;
            }
            return ret;
        }

        private void SaveFirstProcess(WorRequestDTO request)
        {
            // 工艺路线首站报工时，update WO_BASE. WO_START_TIME & WO_BASE.INPUT_QTY
            UpdateWoBase(request);
            // 工艺路线首站报工时，检查WOR_STATUS是否存在工单的记录，不存在时按途程中把报工的工序插入记录。PASS_QTY、FAIL_QTY初始为0，首站的INPUT _QTY=WO_BASE. TARGET_QTY- SCRAP_QTY.
            AddWorStatus(request);
        }

        private void UpdateWoBase(WorRequestDTO request)
        {
            var wobase = _repWobase.GetWoBaseByWo(request.WorkOrder);
            wobase.WoStartTime = DateTime.Now;
            // modify ITC-1755-0182
            wobase.InputQty += request.InputPassQty;
        }

        private void AddWorStatus(WorRequestDTO request)
        {
            var wo = request.WorkOrder;
            var editor = request.Editor;
            var wobase = _repWobase.GetWoBaseByWo(wo);
            var worstatus = _repWorStatus.GetWorStatusByWo(wo);
            if (worstatus == null)
            {
                var workflowid = GetWorkFlowByWo(wo).Id;
                var workflowsteplst = _repWorkflowStep.GetWorkFlowStep(workflowid).ToList();
                IList<int> tmpprocesslst = new List<int>();

                foreach (var item in workflowsteplst)
                {
                    var flag = false;
                    var starttime = (dynamic)null;
                    var inputqty = 0;
                    var scrapqty = 0;
                    if (wobase.ScrapQty != null)
                    {
                        scrapqty = (int)wobase.ScrapQty;
                    }
                    if (item.Process == null)
                    {
                        starttime = DateTime.Now;
                        inputqty = (int)wobase.TargetQty - scrapqty;
                    }
                    if (item.NextProcess != null)
                    {
                        if (tmpprocesslst.Count > 0)
                        {
                            for (var i = 0; i < tmpprocesslst.Count; i++)
                            {
                                if (item.NextProcess.Id == tmpprocesslst[i])
                                {
                                    flag = true;
                                }
                            }
                        }
                        tmpprocesslst.Add(item.NextProcess.Id);
                        if (flag == false)
                        {
                            WorStatus worStatus = new WorStatus
                            {
                                WorkOrder = wo,
                                PartId = wobase.Part.Id,
                                PartNo = wobase.Part.PartNo,
                                ProductLineId = wobase.ProductLine.Id,
                                ProductLineCode = wobase.ProductLine.Code,
                                ProcessId = item.NextProcess.Id,
                                ProcessCode = item.NextProcess.Code,
                                InputQty = inputqty,
                                PassQty = 0,
                                FailQty = 0,
                                // 与需求确认，目前PROCESS_STARTTIME 和 PROCESS_ENDTTIME 暂不更新
                                // modify ITC-1755-0123
                                // ProcessStartTime = starttime,
                                Editor = editor     
                            };
                            _repWorStatus.Add(worStatus);
                        }
                    }

                }
            }
        }

        private void SaveLastProcess(WorRequestDTO request, int processid)
        {
            // 工艺路线最后工序报工时，更新WO_BASE.OUTPUT_QTY
            var wobase = _repWobase.GetWoBaseByWo(request.WorkOrder);
            var worstatus = _repWorStatus.GetWorStatus(request.WorkOrder, processid);
            // modify ITC-1755-0182
            wobase.OutputQty = worstatus.PassQty;
            // 工艺路线最后工序报工，工单全部完成后，更新WO_BASE.OUTPUT_QTY，WO_CLOSE_TIME，WO_STATUS_ID
            // 工艺路线最后工序报工，工单全部完成后，TERMINAL_PARTS按工单下料。-- 不再自动下料，从下料界面手动下料
            if (wobase.OutputQty == wobase.TargetQty)
            {
                wobase.WoCloseTime = DateTime.Now;
                wobase.StatusId = _repWoStatus.GetStatusidByName("COMPLETE").Id;
            }

        }

        private void UpdateWorStatus(WorRequestDTO request)
        {
            // 良品报工时，更新PASS_QTY累加良品数量，同时当站Pass下一站INPUT_QTY累加相应数量。
            // 不良品报工时，更新FAIL_QTY累加不良品数量，INPUT_QTY减掉对应数量；同时Fail下一站INPUT_QTY累加相应数量。
            var workflowid = GetWorkFlowByWo(request.WorkOrder).Id;
            var processid = GetProcessByTerminal(request.TerminalCode).Id;
            var worstatus = _repWorStatus.GetWorStatus(request.WorkOrder, processid);
            worstatus.PassQty += request.InputPassQty;
            var passprocess = GetNextProcess(workflowid, processid, "PASS");
            if (passprocess != null)
            {
                var passworstatus = _repWorStatus.GetWorStatus(request.WorkOrder, passprocess.Id);
                passworstatus.InputQty += request.InputPassQty;
            }
            worstatus.FailQty += request.InputFailQty;
            worstatus.InputQty -= request.InputFailQty;
            var failprocess = GetNextProcess(workflowid, processid, "FAIL");
            if (failprocess != null)
            {
                var failworstatus = _repWorStatus.GetWorStatus(request.WorkOrder, failprocess.Id);
                failworstatus.InputQty += request.InputFailQty;
            }
        }

        private Process GetNextProcess(int workflowid, int processid, string condition)
        {
            var setting = _repSetting.GetSettingByProgram("NextProcessCondition").ToList();
            var conditionid = -1;
            foreach (var item in setting)
            {
                if (item.Name == condition)
                {
                    conditionid = item.Id;
                    break;
                }               
            }
            var workflowstep = _repWorkflowStep.GetWorkFlowStepByProcessIdAndCondition(workflowid, processid, conditionid);
            if (workflowstep != null)
            {
                return workflowstep.NextProcess;
            }
            return null;
            
        }

        private void AddWorInfo(WorRequestDTO request, ICollectionSpecItemValue item, int usedqty)
        {
            var process = _repTerminal.GetTerminal(request.TerminalCode);
            var itempart = _repPart.Get(item.CandidateId);
            var scanpart = _repPart.Get(item.CandidateId);
            var wobase = _repWobase.GetWoBaseByWo(request.WorkOrder);
            // modify ITC-1755-0158
            WorInfo worinfo = new WorInfo
            {
                WorkOrder = request.WorkOrder,
                Type = wobase.Type,
                ProcessId = process.Id,
                ProcessCode = process.Code,
                ItemPartId = item.CandidateId,
                ItemPartNo = itempart.PartNo,
                ScanPartId = scanpart.Id,
                ScanPartNo = scanpart.PartNo,
                ScanPartValue = item.Value,
                UsedQty = usedqty,
                Editor = request.Editor
            };
            _repworInfo.Add(worinfo);
        }

        private int AddWorLog(WorRequestDTO request)
        {
            var wobase = _repWobase.GetWoBaseByWo(request.WorkOrder);
            var terminal = _repTerminal.GetTerminal(request.TerminalCode);
            // 插入WOR_LOG，其中，勾选维修回流板卡报工时，REFLOW为1，否则为0。
            var reflow = 0;
            if (request.IsReflow == true)
            {
                reflow = 1;
            }
            if (request.InputPassQty != 0)
            {
                WorLog passworlog = new WorLog
                {
                    WorkOrder = request.WorkOrder,
                    PartId = wobase.Part.Id,
                    PartNo = wobase.Part.PartNo,
                    Qty = request.InputPassQty,
                    ProcessId = terminal.Process.Id,
                    ProcessCode = terminal.Process.Code,
                    ProductLineId = terminal.ProductLine.Id,
                    ProductLineCode = terminal.ProductLine.Code,
                    TerminalId = terminal.Id,
                    TerminalCode = request.TerminalCode,
                    Status = "Pass",
                    Reflow = reflow,
                    Remark = request.Remark,
                    Editor = request.Editor
                };
                _repWorLog.Add(passworlog);
            }
            if (request.InputFailQty != 0)
            {
                var sn = "";
                if (request.SN != null && request.SN != "")
                {
                    sn = request.SN;
                }

                WorLog failworlog = new WorLog
                {
                    WorkOrder = request.WorkOrder,
                    SerialNumber = sn,
                    PartId = wobase.Part.Id,
                    PartNo = wobase.Part.PartNo,
                    Qty = request.InputFailQty,
                    ProcessId = terminal.Process.Id,
                    ProcessCode = terminal.Process.Code,
                    ProductLineId = terminal.ProductLine.Id,
                    ProductLineCode = terminal.ProductLine.Code,
                    TerminalId = terminal.Id,                    
                    TerminalCode = request.TerminalCode,
                    Status = "Fail",
                    Reflow = reflow,
                    Remark = request.Remark,
                    Editor = request.Editor                    
                };
                _repWorLog.Add(failworlog);
            }
            var worlog = _repWorLog.Query().OrderByDescending(o => o.Cdt).FirstOrDefault();
            return worlog.Id;                   
        }


        private void AddWorDefect(WorRequestDTO request, int logid)
        {
            var terminal = _repTerminal.GetTerminal(request.TerminalCode);
            // 不良报工时，不良信息写入WOR_DEFECT
            var sn = "";
            if (request.SN != null && request.SN != "")
            {
                sn = request.SN;
            }
            if (request.SymptomLst.Count > 0)
            {
                foreach (var item in request.SymptomLst)
                {
                    WorDefect worDefect = new WorDefect
                    {
                        WorkOrder = request.WorkOrder,
                        SerialNumber = sn,
                        TestWorLogId = logid,
                        DefectId = item.Id,
                        DefectCode = item.Code,
                        ProcessId = terminal.Process.Id,
                        ProcessCode = terminal.Process.Code,
                        Remark = "",
                        Editor = request.Editor
                    };
                    _repworDefect.Add(worDefect);
                } 
            }          
        }

        public void CheckWorRepair(string sn)
        {
            // 刷入序号在WOR_DEFECT存在记录，报错：此序号已打过不良。
            var wordefectlst = _repworDefect.GetWorDefects(sn).ToList();
            if (wordefectlst.Count > 0)
            {
                throw new BizException("CHK041");
            }
        }
    }
}