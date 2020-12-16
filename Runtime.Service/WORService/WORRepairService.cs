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
using IIMes.Services.Runtime.Model.Quality;
using System;

namespace IIMes.Services.Runtime.Services
{
    public class WORRepairService : IWORRepairService
    {
        private readonly IRepository<Symptom> _repSymptom;
        private readonly IRepository<WorLog> _repWorLog;
        private readonly IRepository<WorDefect> _repWorDefect;
        private readonly IRepository<SymptomType> _repSymptomType;
        private readonly IRepository<WorRepairLog> _repWorRepairLog;
        private readonly IRepository<WorRepairLogDetail> _repWorRepairLogDetail;
        private readonly IRepository<Cause> _repCause;
        private readonly IRepository<Duty> _repDuty;
        private readonly IRepository<RepairType> _repRepairType;
        private readonly IRepository<Part> _repPart;
        private readonly IRepository<WorInfo> _repWorInfo;
        private readonly IRepository<WorRepairKeyparts> _repWorRepairKeyparts;
        private readonly IRepository<WoBase> _repWoBase;
        public WORRepairService(IRepository<Symptom> repSymptom,
                                IRepository<WorLog> repWorLog,
                                IRepository<WorDefect> repWorDefect,
                                IRepository<SymptomType> repSymptomType,
                                IRepository<WorRepairLog> repWorRepairLog,
                                IRepository<WorRepairLogDetail> repWorRepairLogDetail,
                                IRepository<Cause> repCause,
                                IRepository<Duty> repDuty,
                                IRepository<RepairType> repRepairType,
                                IRepository<Part> repPart,
                                IRepository<WorInfo> repWorInfo,
                                IRepository<WorRepairKeyparts> repWorRepairKeyparts,
                                IRepository<WoBase> repWoBase)
        {
            _repSymptom = repSymptom;   
            _repWorLog = repWorLog;
            _repWorDefect = repWorDefect;
            _repSymptomType = repSymptomType;
            _repWorRepairLog = repWorRepairLog;
            _repWorRepairLogDetail = repWorRepairLogDetail;
            _repCause = repCause;
            _repDuty = repDuty;
            _repRepairType = repRepairType;
            _repPart = repPart;
            _repWorInfo = repWorInfo;
            _repWorRepairKeyparts = repWorRepairKeyparts;
            _repWoBase = repWoBase;
        }

        // 刷入SN时调用，带出已检测的不良信息
        public WorRepairInfoDTO InputSN(WorRepairRequestDTO request)
        {
            WorRepairInfoDTO repairInfo = new WorRepairInfoDTO();
            // 工单，物料，不良生产线，不良工序，不良工作站
            var worlog = GetDefectWorLogInfo(request.SN);
            if (worlog != null)
            {
                IList<WorDefectInfoDTO> wordefectinfolst = new List<WorDefectInfoDTO>();
                // 不良现象
                wordefectinfolst = GetDefectSymptom(worlog.Id);
                // 维修信息
                var repairiteminfo = GetRepairItemInfo(request.SN, worlog.Id);
                repairInfo = GetRepairInfo(worlog, wordefectinfolst, repairiteminfo);
            }
            else
            {
                throw new BizException("CHK042");
            }

            return repairInfo;
        }

        private WorLog GetDefectWorLogInfo(string sn)
        {
            return _repWorLog.GetWorLog(sn); 
        } 

        private IList<WorDefectInfoDTO> GetDefectSymptom(int testworlogid)
        {
            //不良现象来源： WOR_DEFECT,根据SN从WOR_LOG表中查找最新一条记录的ID关联WOR_DEFECT表的[TEST_WOR_LOG_ID]找到对应的多条不良记录。
            var wordefectlst = _repWorDefect.GetWorDefectsByLogId(testworlogid).ToList();
            if (wordefectlst != null && wordefectlst.Count > 0)
            {
                var DefectLst = new List<WorDefectInfoDTO>();
                for (int j = 0; j < wordefectlst.Count; j++ )
                {
                    WorDefectInfoDTO defectinfo = new WorDefectInfoDTO();
                    var ressymptom = _repSymptom.GetSymptom(wordefectlst[j].DefectCode);
                    var ressymptomtype = _repSymptomType.Get(ressymptom.SymptomType.Id);
                    defectinfo.Defect = new CommonDTO();
                    defectinfo.WorDefectId = wordefectlst[j].Id;
                    defectinfo.Defect.Id = ressymptom.Id;
                    defectinfo.Defect.Code = ressymptom.Code;
                    defectinfo.Defect.Name = ressymptom.Name;
                    defectinfo.DefectRemark = wordefectlst[j].Remark;
                    defectinfo.DefectType = ressymptomtype.Name;

                    DefectLst.Add(defectinfo);
                }
                return DefectLst;
            }
            else
            {
                throw new BizException("CHK009");
            }            
        }

        private WorRepairItemDTO GetRepairItemInfo(string sn, int testworlogid)
        {
            WorRepairItemDTO RepairItem = new WorRepairItemDTO();
            var resrepairlog = _repWorRepairLog.GetRepairLogByTestWorLogId(sn, testworlogid);
            if (resrepairlog != null && resrepairlog.UpdateTime != null)
            {
                throw new BizException("CHK045");
            }
            if (resrepairlog != null)
            {
                var resrepairlogdetaillst = _repWorRepairLogDetail.GetRepairLogDetail(resrepairlog.Id).ToList();
                if (resrepairlogdetaillst != null && resrepairlogdetaillst.Count > 0)
                {
                    WorRepairItemDTO repairitem = new WorRepairItemDTO
                    {
                        RepairItemInfo = new List<WorRepairItemInfoDTO>()
                    };

                    foreach (var item in resrepairlogdetaillst)
                    {
                        WorRepairItemInfoDTO repairiteminfo = new WorRepairItemInfoDTO
                        {
                            SN = sn
                        };
                        var symptom = _repSymptom.GetSymptom(item.DefectCode);
                        repairiteminfo.Symptom = new CommonDTO
                        {
                            Id = item.DefectId,
                            Code = item.DefectCode,
                            Name = symptom.Name
                        };
                        var Cause = _repCause.GetCause(item.CauseCode);                        
                        repairiteminfo.Cause = new CommonDTO
                        {
                            Id = item.CauseId,
                            Code = item.CauseCode,
                            Name = Cause.Name
                        };
                        var Duty = _repDuty.GetDuty(item.DutyCode);                        
                        repairiteminfo.Duty = new CommonDTO
                        {
                            Id = Convert.ToInt32(item.DutyId),
                            Code = item.DutyCode,
                            Name = Duty.Name
                        };
                        var RepairType = _repRepairType.GetRepairType(item.RepairtypeCode);                        
                        repairiteminfo.RepairType = new CommonDTO
                        {
                            Id = item.RepairtypeId,
                            Code = item.RepairtypeCode,
                            Name = RepairType.Name
                        };
                        repairiteminfo.Location = item.Location;
                        repairiteminfo.Description = item.Remark;
                        repairitem.RepairLogId = resrepairlog.Id;
                        repairiteminfo.RepairLogDetailId = item.Id;

                        repairitem.RepairItemInfo.Add(repairiteminfo);
                    }
                    RepairItem = repairitem;
                }
            }
            return RepairItem;            
        }

        private WorRepairInfoDTO GetRepairInfo(WorLog worlog, IList<WorDefectInfoDTO> wordefecinfolst, WorRepairItemDTO worrepairiteminfo)
        {
            var part = _repPart.Get(worlog.PartId);
            WorRepairInfoDTO repairinfo = new WorRepairInfoDTO
            {
                WorkOrder = worlog.WorkOrder,
                Part = new PartItemDTO()
            };
            if (part != null)
            {
                repairinfo.Part.PartName = part.PartName;
                repairinfo.Part.PartNo = part.PartNo;
            }            
            repairinfo.DefectPdLineCode = worlog.ProductLineCode; //不良生产线
            repairinfo.DefectProcess = new CommonDTO
            {
                Id = worlog.ProcessId, //不良站点
                Code = worlog.ProcessCode
            };
            repairinfo.DefectTerminalCode = worlog.TerminalCode;

            repairinfo.DefectList = new List<WorDefectInfoDTO>();
            if (wordefecinfolst != null && wordefecinfolst.Count > 0)
            {
                repairinfo.DefectList = wordefecinfolst;
            }

            repairinfo.RepairInfo = new WorRepairItemDTO();

            if (worrepairiteminfo != null)
            {
                repairinfo.RepairInfo = worrepairiteminfo;
            }
            repairinfo.RepairProcess = new CommonDTO();                       
            // repairinfo.RepairProcess.Id = (int)DefectProductStatus.NextProcessId; //维修站ProcessId 暂时注掉，2020.10.14
            // repairinfo.RepairProcess.Code = DefectProductStatus.NextProcessCode; //维修站ProcessCode 暂时注掉，2020.10.14

            repairinfo.TestProductLogId = worlog.Id;

            return repairinfo;
        }

        // 维修不良时调用
        [Transactional]
        public WorRepairItemDTO SaveRepair(WorRepairItemInfoDTO request)
        {
            // finished: 判断是否完成维修
            bool finished = false;
            var repairlogid = SaveRepairLog(finished, request, -1);
            SaveRepairDetailLog(request, repairlogid);            
            WorRepairItemDTO repairitemInfo = new WorRepairItemDTO();
            repairitemInfo = GetRepairItemInfo(request.SN, request.TestWorLogId);
            return repairitemInfo;
        }

        private int SaveRepairLog(bool finished, WorRepairItemInfoDTO request, int repairlogid)
        {
            var repairlogId = repairlogid;
            var repairitem = request;
            if (repairitem != null && finished == false)
            {
                // 维修后，数据保存在REPAIR_LOG和REPAIR_LOG_DETAIL表
                // 当WOR_REPAIR_LOG中REPAIR_WOR_LOG_ID为null时表示上次维修未完成，不需要新增记录，否则增加一条新记录。维修详细结果保存在WOR_REPAIR_LOG_DETAIL表
                var repairlog = _repWorRepairLog.GetRepairLogByProcess(request.SN, repairitem.RepairProcess.Code);
                var worlog = _repWorLog.Get(repairitem.TestWorLogId);

                if (repairlog == null || repairlog.RepairWorLogId != null || repairlog.UpdateTime != null)
                {
                    WorRepairLog repairloginfo = new WorRepairLog
                    {
                        WorkOrder = repairitem.WorkOrder,
                        SerialNumber = repairitem.SN,
                        TestWorLogId = repairitem.TestWorLogId,
                        ProcessId = repairitem.RepairProcess.Id,
                        ProcessCode = repairitem.RepairProcess.Code,
                        Repairer = repairitem.Editor,
                        Cdt = DateTime.Now
                    };
                    _repWorRepairLog.Add(repairloginfo);
                }
                repairlog = _repWorRepairLog.GetRepairLogByProcess(request.SN, repairitem.RepairProcess.Code);
                repairlogId = repairlog.Id;
            }
            // 判断是否完成维修
            if (finished == true)
            {
                var repairlog = _repWorRepairLog.Get(repairlogid);
                var repworlog = _repWorLog.GetWorLog(repairlog.SerialNumber);

                repairlog.RepairWorLogId = repworlog.Id;
                repairlog.UpdateTime = DateTime.Now;
            }

            return repairlogId;            
        }

        private void SaveRepairDetailLog(WorRepairItemInfoDTO request, int repairlogid)
        {
            var repairlogId = repairlogid;
            var repairitem = request;

            //维修后，数据保存在WOR_REPAIR_LOG和WOR_REPAIR_LOG_DETAIL表
            //当WOR_REPAIR_LOG中REPAIR_WOR_LOG_ID为null时表示上次维修未完成，不需要新增记录，否则增加一条新记录。维修详细结果保存在WOR_REPAIR_LOG_DETAIL表
            WorRepairLogDetail repairlogdetail = new WorRepairLogDetail
            {
                WorRepairLogId = repairlogId,
                DefectId = repairitem.Symptom.Id, //不良现象
                DefectCode = repairitem.Symptom.Code,
                CauseId = repairitem.Cause.Id, //不良原因
                CauseCode = repairitem.Cause.Code,
                RepairtypeId = repairitem.RepairType.Id, //维修方式
                RepairtypeCode = repairitem.RepairType.Code,
                DutyId = repairitem.Duty.Id, //责任类别
                DutyCode = repairitem.Duty.Code,
                Remark = repairitem.Description,
                Location = repairitem.Location,
                Editor = repairitem.Editor,
                Cdt = DateTime.Now
            };
            _repWorRepairLogDetail.Add(repairlogdetail);
        }

        // 点击完成，保存过站
        [Transactional]
        public string FinishRepair(WorRepairFinishRequestDTO request)
        {
            CheckFinishedRepair(request.DefectIdList, request.RepairLogId);
            bool finished = true;
            WorRepairItemInfoDTO repairiteminfo = new WorRepairItemInfoDTO();
            SaveRepairLog(finished, repairiteminfo, request.RepairLogId);
            SaveRepairKeyParts(finished, request.RepairLogId, null);
            // 下一站 ？？？
            string nwc = "";
            return nwc;
        }

        private void CheckFinishedRepair(IList<int> defectIdList, int repairlogid)
        {
            var isFinished = false;
            var resrepairlogdetail = _repWorRepairLogDetail.GetRepairLogDetail(repairlogid).ToList();

            if (resrepairlogdetail != null && resrepairlogdetail.Count > 0)
            {
                bool flag = false;
                for (int j = 0; j < defectIdList.Count; j++)
                {
                    flag = false;
                    foreach (var item in resrepairlogdetail)
                    {
                        if (item.DefectId == defectIdList[j])
                        {
                            flag = true;
                        }
                    }
                    if (flag == false)
                    {
                        break;
                    }
                }
                isFinished = flag;
            }
            //true: 不良全部维修完成； false: 不良未完成维修
            if (isFinished == false)
            {
                throw new BizException("CHK043");
            }          
        }

        // 保存新增不良现象时调用
        [Transactional]
        public IList<WorDefectInfoDTO> SaveDefect(WorDefectRequestDTO request)
        {
            if (request.DefectLst != null && request.DefectLst.Count > 0)
            {

                for (int i = 0; i < request.DefectLst.Count; i++)
                {
                    var repwordefectlst = _repWorDefect.GetWorDefectsByDefectCode(request.SN, request.DefectLst[i].Code, request.TestProductLogId).ToList();
                    if (repwordefectlst.Count > 0)
                    {
                        throw new BizException("CHK044");
                    }

                    WorDefect defect = new WorDefect
                    {
                        WorkOrder = request.WorkOrder,
                        SerialNumber = request.SN,
                        TestWorLogId = request.TestProductLogId,
                        DefectCode = request.DefectLst[i].Code,
                        ProcessCode = request.RepairProcess.Code,
                        Remark = request.DefectLst[i].Remark,
                        Editor = request.Editor,
                        Cdt = DateTime.Now

                    };
                    _repWorDefect.Add(defect);
                }
            }

            IList<WorDefectInfoDTO> wordefectinfo = new List<WorDefectInfoDTO>();
            wordefectinfo = GetDefectSymptom(request.TestProductLogId);
            return wordefectinfo;
        }

        // 换料时获取不良物料序号时调用
        public IList<PartCheckDTO> GetDefectKeyParts(WorDefectKeypartsRequestDTO request)
        {
            var sn = request.SN;

            IList<PartCheckDTO> partcheckLst = new List<PartCheckDTO>();
            var wordefect = _repWorDefect.GetWorDefects(sn).ToList()[0];
            if (wordefect != null)
            {
                var worinfolst = _repWorInfo.GetWorInfo(wordefect.WorkOrder).ToList();
                IList<int> itempartidLst = new List<int>();
                if (worinfolst != null && worinfolst.Count > 0)
                {
                    foreach (var items in worinfolst)
                    {
                        itempartidLst.Add(items.ItemPartId);
                    }
                }
                // 去掉重复的itempartid
                HashSet<int> hs = new HashSet<int>(itempartidLst);
                itempartidLst = hs.ToList();

                for (var i = 0; i < itempartidLst.Count; i++ )
                {
                    PartCheckDTO partcheck = new PartCheckDTO();
                    var part = _repPart.Get(itempartidLst[i]);
                    partcheck.PartId = part.Id;
                    partcheck.PartNo = part.PartNo;
                    partcheck.PartName = part.PartName;
                    var spec1 = "";
                    if (part.Spec1 != null)
                    {
                        spec1 = part.Spec1;
                    }
                    partcheck.SPEC1 = spec1;
                    partcheck.MatchRule = part.MatchRule;
                    // partcheck.Qty = bomitem.ItemCount; 先注掉 2020.10.14
                    // var product_info = RepProductInfo.FindByLambda(o => o.ItemPartNo == part.PartNo && o.SerialNumber == sn);
                    // partcheck.PartSNs = new List<string>();
                    // if (product_info != null && product_info.Count > 0)
                    // {
                    //     foreach (var items in product_info)
                    //     {
                    //         partcheck.PartSNs.Add(items.ScanPartValue);
                    //     }
                    // }

                    partcheckLst.Add(partcheck);
                }

            }

            return partcheckLst;
        }

        // 换料时保存时调用
        [Transactional]
        public void SaveNewKeyParts(WorDefectKeypartsSaveRequestDTO request)
        {
            // CheckNewPartForRepair(); 目前不做检查
            // GetMatchedPart(); 目前不做检查
            // CheckUnique(); 目前不做检查
            WorKeyPartsItemDTO workeypartsitem = new WorKeyPartsItemDTO();
            workeypartsitem.SN = request.SN;
            workeypartsitem.DefectPart = request.DefectPart;
            workeypartsitem.NewPart = request.NewPart;
            workeypartsitem.Remark = request.Remark;
            workeypartsitem.TerminalCode = request.TerminalCode;
            workeypartsitem.Editor = request.Editor;
            AddWorInfo(workeypartsitem);
            // 保存换料信息
            bool finished = false;
            SaveRepairKeyParts(finished, -1, workeypartsitem);
        }

        private void AddWorInfo(WorKeyPartsItemDTO request)
        {
            var wordefect = _repWorDefect.GetWorDefects(request.SN).ToList()[0];
            var wobase = _repWoBase.GetWoBaseByWo(wordefect.WorkOrder);
            WorInfo worinfo = new WorInfo
            {
                WorkOrder = wordefect.WorkOrder,
                Type = wobase.Type,
                ProcessId = wordefect.ProcessId,
                ProcessCode = wordefect.ProcessCode,
                ItemPartId = request.NewPart.PartId,
                ItemPartNo = request.NewPart.PartNo,
                ScanPartId = request.NewPart.PartId,
                ScanPartNo = request.NewPart.PartNo,
                ScanPartValue = request.NewPart.PartSn,
                UsedQty = 1,
                Editor = request.Editor
            };
            _repWorInfo.Add(worinfo);            
        }

        private void SaveRepairKeyParts(bool finished, int repairlogid, WorKeyPartsItemDTO keypartsitem)
        {
            if (keypartsitem != null)
            {
                if (keypartsitem != null && finished == false)
                {
                    var wordefect = _repWorDefect.GetWorDefects(keypartsitem.SN).ToList()[0];
                    var wobase = _repWoBase.GetWoBaseByWo(wordefect.WorkOrder);
                    WorRepairKeyparts repairkeyparts = new WorRepairKeyparts
                    {
                        WorkOrder = wobase.WorkOrder,
                        SerialNumber = keypartsitem.SN,
                        OldPartId = keypartsitem.DefectPart.PartId,
                        OldPartNo = keypartsitem.DefectPart.PartNo,
                        NewPartId = keypartsitem.NewPart.PartId,
                        NewPartNo = keypartsitem.NewPart.PartNo,
                        NewPartSn = keypartsitem.NewPart.PartSn,
                        Remark = keypartsitem.Remark,
                        Editor = keypartsitem.Editor,
                        Cdt = DateTime.Now
                    };
                    _repWorRepairKeyparts.Add(repairkeyparts);

                }

                // 判断是否完成维修
                if (finished == true)
                {
                    var repairkeypartslst = _repWorRepairKeyparts.GetWorRepairKeyparts(keypartsitem.SN).ToList();
                    if (repairkeypartslst != null && repairkeypartslst.Count > 0)
                    {
                        foreach (var item in repairkeypartslst)
                        {
                            item.RepairLogId = repairlogid;
                        }
                    }

                }
            }
        }

        // 获取维修历史
        public IList<WorRepairHistoryDTO> GetRepairHistory(string sn)
        {
            return GetRepairLog(sn);
        }

        // 获取维修记录表数据
        protected IList<WorRepairHistoryDTO> GetRepairLog(string sn)
        {
            IList<WorRepairHistoryDTO> RepairHistoryLst = new List<WorRepairHistoryDTO>();

            var repairloglst = _repWorRepairLog.GetRepairLogByUdt(sn).ToList();

            if (repairloglst != null && repairloglst.Count > 0)
            {
                foreach (var item in repairloglst)
                {
                    WorRepairHistoryDTO repairhistory = new WorRepairHistoryDTO();
                    var worlog = _repWorLog.Get(item.TestWorLogId);
                    var wordefect = _repWorDefect.GetWorDefectsByLogId(item.TestWorLogId).ToList()[0];
                    repairhistory.DefectProcessCode = worlog.ProcessCode;
                    repairhistory.DefectTime = wordefect.Cdt.ToString();
                    repairhistory.RepairTime = item.UpdateTime.ToString();
                    repairhistory.Editor = item.Repairer;

                    RepairHistoryLst.Add(repairhistory);
                }
            }

            return RepairHistoryLst;

        }

        // 获取换料历史
        public IList<WorKeyPartsItemDTO> GetKeyPartsHistory(string sn)
        {
            return GetKeyPartsListBySn(sn);
        }

        // 获取维修换料记录表数据
        protected IList<WorKeyPartsItemDTO> GetKeyPartsListBySn(string sn)
        {
            IList<WorKeyPartsItemDTO> RepairKeypartsLst = new List<WorKeyPartsItemDTO>();
            var repairkeyparts = _repWorRepairKeyparts.GetWorRepairKeyparts(sn).ToList();
            if (repairkeyparts != null && repairkeyparts.Count > 0)
            {
                for (int m = 0; m < repairkeyparts.Count; m++)
                {
                    WorKeyPartsItemDTO RepairKeyparts = new WorKeyPartsItemDTO();
                    RepairKeyparts.DefectPart = new ScanPartDTO();
                    RepairKeyparts.DefectPart.PartNo = repairkeyparts[m].OldPartNo;
                    var oldpart = _repPart.Get(repairkeyparts[m].OldPartId);
                    RepairKeyparts.DefectPart.PartName = oldpart.PartName;
                    var oldspec1 = "";
                    if (oldpart.Spec1 != null)
                    {
                        oldspec1 = oldpart.Spec1;
                    }
                    RepairKeyparts.DefectPart.PartSPEC1 = oldspec1;
                    RepairKeyparts.NewPart = new ScanPartDTO();
                    RepairKeyparts.NewPart.PartNo = repairkeyparts[m].NewPartNo;
                    var newpart = _repPart.Get(repairkeyparts[m].NewPartId);                    
                    RepairKeyparts.NewPart.PartName = newpart.PartName;
                    var newspec1 = "";
                    if (newpart.Spec1 != null)
                    {
                        newspec1 = newpart.Spec1;
                    }
                    RepairKeyparts.NewPart.PartSPEC1 = newspec1;
                    RepairKeyparts.NewPart.PartSn = repairkeyparts[m].NewPartSn;

                    RepairKeypartsLst.Add(RepairKeyparts);
                }
            }
            return RepairKeypartsLst;

        }

        // 删除不良现象时调用
        public void DeleteDefect(int id)
        {
            var old = _repWorDefect.Get(id);
            if (old == null)
            {
                return;
            }
            else
            {
                // _repWorDefect.Delete(old);
                _repWorDefect.DeleteByLambda(p => p.Id == id);
            }
        }

        // 删除维修信息时调用
        public void DeleteRepairInfo(int id)
        {
            var old = _repWorRepairLogDetail.Get(id);
            if (old == null)
            {
                return;
            }
            else
            {
                // _repWorRepairLogDetail.Delete(old);
                _repWorRepairLogDetail.DeleteByLambda(p => p.Id == id);
            }
        }
    }
}