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
using AutoMapper;
using IIMes.Infrastructure.Aspect;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Exception;
using IIMes.Infrastructure.Service;
using IIMes.Services.Core.Model;
using IIMes.Services.Maintain.Interface;
using IIMes.Services.Maintain.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProtoBuf.Grpc.Configuration;

namespace IIMes.Services.Maintain.Services
{
    public partial class WorkflowService : BaseMaintainService<SWorkflow, SWorkflowDTO>, IWorkflowService
    {
        private readonly IRepository<SWorkflow> _repWorkflow;
        private readonly IRepository<SWorkflowBase> _repWorkflowBase;
        private readonly IRepository<SWorkflowStep> _repWorkflowStep;
        private readonly IRepository<WoBase> _repWoBase;
        private readonly IBopBaseService _bopBaseService;

        public WorkflowService(
            IMapper mapper,
            IRepository<SWorkflow> rep,
            IRepository<SWorkflowBase> repWorkflowBase,
            IRepository<SWorkflowStep> repWorkflowStep,
            IRepository<WoBase> repWoBase,
            IBopBaseService bopBaseService)
        : base(rep, mapper)
        {
            _repWorkflow = rep;
            _repWorkflowBase = repWorkflowBase;
            _repWorkflowStep = repWorkflowStep;
            _repWoBase = repWoBase;
            _bopBaseService = bopBaseService;
        }

        [Transactional]
        public object SaveWorkflow(dynamic request)
        {
            var workflowJson = new Dictionary<string, dynamic>();
            workflowJson["nodes"] = request.nodes;
            workflowJson["links"] = request.links;

            var x = new SWorkflow();
            x.WorkflowJson = JsonConvert.SerializeObject(workflowJson);
            x.Version = request.version;
            x.Description = request.desc;
            x.Editor = request.editor;
            x.Id = request.id == null ? 0 : request.id;
            x.Status = "0";

            int? partId = request.partid;
            int familyId = request.familyid;
            string routeName = request.routeName;

            // 根据RouteName, familyId, partId 获取WorkflowBaseId (partId为空 则不作为查询条件)
            var resWorkflowBases = _repWorkflowBase.Query().Where(p => p.Name == routeName && p.SFamily.Id == familyId);
            resWorkflowBases = partId == null ? resWorkflowBases.Where(p => p.SPart == null) : resWorkflowBases.Where(p => p.SPart.Id == partId);
            var resWorkflowBase = resWorkflowBases.OrderByDescending(p => p.LastVersion).FirstOrDefault();
            if (resWorkflowBase == null)
            {
                var temp = new SWorkflowBase();
                temp.Name = routeName;
                temp.SPart = partId == null ? null : new SPart() { Id = (int)partId };
                temp.SFamily = new SFamily() { Id = familyId };
                temp.LastVersion = null;
                temp.Editor = request.editor;
                x.SWorkflowBase = new SWorkflowBase() { Id = (int)_repWorkflowBase.Add(temp) };
            }
            else
            {
                x.SWorkflowBase = new SWorkflowBase() { Id = resWorkflowBase.Id };
            }

            // 版本定义顺序 VersionSequence
            var workflow = _repWorkflow.Query().Where(p => p.SWorkflowBase.Id == x.SWorkflowBase.Id)
                .OrderByDescending(p => Convert.ToInt32(p.VersionSequence)).FirstOrDefault();
            var maxVersionSequence = workflow == null ? 0 : Convert.ToInt32(workflow.VersionSequence);

            // Id为0时。新增Workflow,否则更新
            if (x.Id == 0)
            {
                // 新增时, S_WORKFLOW.VERSION_SEQUENCE栏位 +1
                x.VersionSequence = Convert.ToString(maxVersionSequence + 1);
                x.Id = (int)_repWorkflow.Add(x);
            }
            else
            {
                // 更新时，S_WORKFLOW.VERSION_SEQUENCE栏位 不变
                x.VersionSequence = _repWorkflow.Get(x.Id).VersionSequence;
                x.Udt = DateTime.Now;
                _repWorkflow.Update(x);
            }

            // TODO:待修改
            SaveWorkflowStep(x.Id, request);

            // 返回前台需要的数据结构
            return FindById(x.Id);
        }

        public object CheckBeforeAdd(dynamic request)
        {
            // 若 id为空，表示新增；不为空，表示更改;
            int? id = request.id;
            int? partId = request.partid;
            int familyId = request.familyid;
            string version = request.version;

            // 根据familyId, partId 获取WorkflowBaseId (partId为空 则不作为查询条件)
            var resWorkflowBases = _repWorkflowBase.Query().Where(p => p.SFamily.Id == familyId);
            resWorkflowBases = partId == null ? resWorkflowBases.Where(p => p.SPart == null) : resWorkflowBases.Where(p => p.SPart.Id == partId);
            var resWorkflowBase = resWorkflowBases.OrderByDescending(p => p.LastVersion).FirstOrDefault();
            if (resWorkflowBase != null)
            {
                var workflows = _repWorkflow.Query().Where(p => p.SWorkflowBase.Id == resWorkflowBase.Id);
                // 发布态（多个）
                var workflowPublish = workflows.Where(p => p.Status == "1");
                // 编辑态 (唯一)
                var workflowEdit = workflows.Where(p => p.Status == "0");

                // 增加保护：新增或更改时, 此版本工艺路线已经发布, 报错
                var publishVersionList = workflowPublish.Select(p => p.Version).ToList();
                if (publishVersionList.Contains(version))
                {
                    throw new BizException("CHK030");
                }

                // 增加保护：新增时, 存在编辑态的工艺路线时，需要确认后才能新增
                if (workflowEdit.Count() > 0 && id == null)
                {
                    var worflowId = workflowEdit.FirstOrDefault().Id;
                    return worflowId;
                }
            }

            return null;
        }

        // SaveWorkflowStep
        [Transactional]
        public void SaveWorkflowStep(int id, dynamic request)
        {
            var nodes = request.nodes;
            var links = request.links;
            // 删除旧数据
            _repWorkflowStep.DeleteByLambda(p => p.SWorkflow.Id == id);
            // 新增
            var list = new List<SWorkflowStep>();

            // ITC-1755-0162
            var linksList = new List<Dictionary<string, object>>();
            foreach (var link in links)
            {
                var xx = new Dictionary<string, object>();
                xx["from"] = link.from;
                xx["type"] = link.type;
                linksList.Add(xx);
            }

            var collection = new List<Dictionary<string, object>>();
            foreach (var node in nodes)
            {
                var ret = linksList.Where(x => x["from"] == node.key);
                if (ret.Count() > 0)
                {
                    foreach (var link in ret)
                    {
                        var xx = new Dictionary<string, object>();
                        xx["key"] = node.key;
                        xx["type"] = link["type"];
                        collection.Add(xx);
                    }
                }
                else
                {
                    var xx = new Dictionary<string, object>();
                    xx["key"] = node.key;
                    xx["type"] = "";
                    collection.Add(xx);
                }
            }

            foreach (var dic in collection)
            {
                var workflowId = id;
                var key = Convert.ToInt32(dic["key"]);
                var type = Convert.ToString(dic["type"]);
                SWorkflowStep workflowStep = GetInfo(links, nodes, key, workflowId, type);

                workflowStep.Editor = request.editor;
                workflowStep.Udt = DateTime.Now;
                list.Add(workflowStep);
            }

            _repWorkflowStep.Add(list);
        }

        private SWorkflowStep GetInfo(dynamic links, dynamic nodes, int key, int workflowId, string type)
        {
            foreach (var item in nodes)
            {
                if (item.key == key)
                {
                    var sWorkflowStep = new SWorkflowStep();
                    // WORKFLOW_ID
                    sWorkflowStep.SWorkflow = new SWorkflow() { Id = workflowId };

                    // PROCESS_ID
                    if (item.process != null && item.process.id != null)
                    {
                        sWorkflowStep.SProcess = new SProcess() { Id = item.process.id };
                    }

                    foreach (var link in links)
                    {
                        if (link.from == key && link.type == type)
                        {
                            // CONDITION_ID
                            var conditionId = link.condition.id;
                            if (conditionId != null)
                            {
                                sWorkflowStep.SSetting = new SSetting() { Id = link.condition.id };
                            }

                            // NEXT_PROCESS_ID
                            foreach (var node in nodes)
                            {
                                if (node.key == link.to)
                                {
                                    sWorkflowStep.SNextProcess = new SProcess() { Id = node.process.id };
                                    break;
                                }
                            }

                            // PRIORITY
                            sWorkflowStep.Priority = link.sequnce;

                            // SPNAME
                            sWorkflowStep.Spname = link.spname;
                            break;
                        }
                    }

                    return sWorkflowStep;
                }
            }

            return null;
        }

        [Transactional]
        public void DeleteWorkflowAndWorkflowStep(int workflowId)
        {
            // 删除Workflow下的所有WorkflowStep
            _repWorkflowStep.DeleteByLambda(p => p.SWorkflow.Id == workflowId);
            // 删除workflow
            Delete(workflowId);
        }

        [Transactional]
        public object CopyList()
        {
            var collection = _repWorkflow.Query();
            var list = new List<dynamic>();
            // 转换成页面需要的结构
            foreach (var item in collection)
            {
                var part = new Dictionary<string, object>();
                if (item.SWorkflowBase.SPart != null)
                {
                    part["id"] = item.SWorkflowBase.SPart.Id;
                    part["part_name"] = item.SWorkflowBase.SPart.PartName;
                }
                else
                {
                    part = null;
                }

                var family = new Dictionary<string, object>();
                if (item.SWorkflowBase.SFamily != null)
                {
                    family["id"] = item.SWorkflowBase.SFamily.Id;
                    family["family_name"] = item.SWorkflowBase.SFamily.Name;
                }
                else
                {
                    family = null;
                }

                var workflowBase = new Dictionary<string, object>();
                workflowBase["id"] = item.SWorkflowBase.Id;
                workflowBase["name"] = item.SWorkflowBase.Name;
                workflowBase["part"] = part;
                workflowBase["family"] = family;

                var root = new Dictionary<string, object>();
                root["id"] = item.Id;
                root["version"] = item.Version;
                root["desc"] = item.Description;
                root["workflowBase"] = workflowBase;

                list.Add(root);
            }

            // 按照WorkflowBaseName,Version排序
            var result = list.OrderBy(x => x["workflowBase"]["name"]).ThenBy(x => x["version"]);
            return result;
        }

        [Transactional]
        public object CheckBeforePublish(SWorkflowBaseDTO request)
        {
            // 如果是part工艺路线，检查是否存在该part的RELEASE\WIP的工单，
            // 如果存在，提示用户发布后这些工单将使用新配置，需要用户确认发布，
            var partId = request.PartId;
            if (partId != null)
            {
                var status = new List<string>() { "RELEASE", "WIP" };
                var woBases = _repWoBase.Query()
                    .Where(p => p.SPart.Id == partId && status.Contains(p.SWoStatus.Name));
                if (woBases.Count() != 0)
                {
                    var woList = woBases.Select(p => p.WorkOrder).ToList<string>();
                    return woList;
                }
            }

            // 如果是family工艺路线，检查是否存在使用了旧版本family工艺路线的RELEASE\WIP工单，
            // 如果存在，提示用户发布后这些工单将使用新配置，需要用户确认发布，
            var familyId = request.FamilyId;
            if (familyId != null && partId == null)
            {
                var status = new List<string>() { "RELEASE", "WIP" };
                var woBases = _repWoBase.Query()
                    .Where(p => p.SWorkflow.SWorkflowBase.SFamily.Id == familyId.Value
                    && p.SWorkflow.SWorkflowBase.SPart == null
                    && status.Contains(p.SWoStatus.Name));
                if (woBases.Count() != 0)
                {
                    var woList = woBases.Select(p => p.WorkOrder).ToList<string>();
                    return woList;
                }
            }

            return null;
        }

        // 发布
        [Transactional]
        public void Publish(WorkflowRequestDTO request)
        {
            // 更新Workflow.Status = "1"
            int id = request.WorflowId;
            var editor = request.Editor;
            var workflow = FindById(id);
            workflow.Status = "1";
            workflow.Editor = editor;
            Update(id, workflow);

            // 更新WorkflowBase.LastVersion
            var workflowBase = _repWorkflowBase.Get(workflow.WorkflowBaseId);
            workflowBase.LastVersion = workflow.Version;
            workflowBase.Udt = DateTime.Now;
            _repWorkflowBase.Update(workflowBase);

            // 新增BopBase
            var bopBase = new SBopBaseDTO();
            bopBase.Part = request.PartId != null ? new SPart() { Id = Convert.ToInt32(request.PartId) } : null;
            bopBase.Family_id = request.FamilyId;
            bopBase.Workflowid = request.WorflowId.ToString();
            bopBase.Editor = request.Editor;
            _bopBaseService.Add(bopBase);

            // 若传入了workorders, 更新WoBase
            var workorders = request.Workorders;
            if (workorders.Count > 0)
            {
                // 将RELEASE\WIP的工单的工艺路线更新为最新版、工艺清单为空
                var count = UpdateWoBase(request);
            }

            return;
        }

        private int UpdateWoBase(WorkflowRequestDTO request)
        {
            var workorders = request.Workorders;
            var worflowId = request.WorflowId;
            var editor = request.Editor;
            var count = 0;
            foreach (var wo in workorders)
            {
                // 获取旧WoBase
                WoBase old = _repWoBase.Query().Where(p => p.WorkOrder == wo).FirstOrDefault();
                // 工艺路线更新为最新版
                old.SWorkflow = new SWorkflow() { Id = worflowId };
                // 工艺清单为空
                old.BopId = null;
                // 编辑人
                old.Editor = editor;
                // 更新
                _repWoBase.Update(old);
                // 数量+1
                count++;
            }

            return count;
        }

        public IList<CommonDTO> GetProcessList(int workflowid)
        {
            var workflowsteplst = _repWorkflowStep.GetWorkFlowStep(workflowid).ToList();
            IList<SProcess> processes = new List<SProcess>();
            foreach (var item in workflowsteplst)
            {
                if (item.SNextProcess != null)
                {
                    processes.Add(item.SNextProcess);
                }
            }

            return GetCommonDTOLst(processes);
        }

        private IList<CommonDTO> GetCommonDTOLst(IList<SProcess> processlst)
        {
            IList<CommonDTO> comlst = new List<CommonDTO>();
            foreach (var process in processlst)
            {
                CommonDTO com = new CommonDTO
                {
                    Id = process.Id,
                    Code = process.Code,
                    Name = process.Name
                };
                comlst.Add(com);
            }

            return comlst;
        }
    }
}
