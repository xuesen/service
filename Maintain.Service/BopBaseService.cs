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
using System.Data.SqlClient;
using System.Linq;
using AutoMapper;
using IIMes.Infrastructure.Aspect;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Exception;
using IIMes.Infrastructure.Hibernate.MSSQL;
using IIMes.Infrastructure.Service;
using IIMes.Services.Core.Model;
using IIMes.Services.Maintain.Interface;
using Newtonsoft.Json;

namespace IIMes.Services.Maintain.Services
{
    public partial class BopBaseService : BaseMaintainService<SBopBase, SBopBaseDTO>, IBopBaseService
    {
        private readonly IRepository<SBopBase> _repBopbase;
        private readonly IRepository<SBop> _repBop;
        private readonly IRepository<SWorkflow> _repWorkflow;
        private readonly IRepository<SBomItem> _repBomItem;
        private readonly IRepository<WoBase> _repWoBase;
        private readonly IRepository<SBopProcessBom> _repBopProcessBom;
        private readonly IRepository<SBopProcessLabel> _repBopProcessLabel;
        private readonly IRepository<SBopSpec> _repBopSpec;
        private readonly IRepository<SBopProcessTestitem> _repBopProcessTestitem;
        private readonly IRepository<SBopProcessSampling> _repBopProcessSampling;
        private readonly IRepository<SPart> _repPart;

        public BopBaseService(
            IRepository<SBopBase> rep,
            IRepository<SBop> repBop,
            IRepository<SBopProcessBom> repBopProcessBom,
            IRepository<SBopProcessLabel> repBopProcessLabel,
            IRepository<SBopProcessSampling> repBopProcessSampling,
            IRepository<SBopProcessTestitem> repBopProcessTestitem,
            IRepository<SBopSpec> repBopSpec,
            IRepository<SBomItem> repBomItem,
            IMapper mapper,
            IRepository<SWorkflow> repWorkflow,
            IRepository<WoBase> repWoBase,
            IRepository<SPart> repPart)
        : base(rep, mapper)
        {
            _repBopbase = rep;
            _repWorkflow = repWorkflow;
            _repBomItem = repBomItem;
            _repWoBase = repWoBase;
            _repBop = repBop;
            _repBopProcessBom = repBopProcessBom;
            _repBopProcessLabel = repBopProcessLabel;
            _repBopSpec = repBopSpec;
            _repBopProcessTestitem = repBopProcessTestitem;
            _repBopProcessSampling = repBopProcessSampling;
            _repPart = repPart;
        }

        public object GetPart()
        {
            var ret = new List<SBopBaseDTO>();

            ret = _repBopbase.ExecNamedSqlQuery<SBopBaseDTO>("GetPart", null).ToList();

            return ret;
        }

        public object GetLastestVersionWorkflow(int wordkflowid)
        {
            // 根据wordkflowid得到workflowjson
            var sWorkflow = _repWorkflow.Get(wordkflowid);
            var workflowjson = JsonConvert.DeserializeObject<object>(sWorkflow.WorkflowJson);
            // 根据wordkflowid得到bopbaseid
            var bopbaseid = _repBopbase.Query().Where(p => p.SWorkflow.Id == wordkflowid && p.Status == "0").FirstOrDefault().Id;
            // 转化格式
            var ret = new Dictionary<string, object>();
            ret["workflowId"] = wordkflowid;
            ret["workflow"] = workflowjson;
            ret["bopbaseid"] = bopbaseid;
            return ret;
        }

        public object GetBomVersion(int partId)
        {
            var parameters = new Dictionary<string, object>
                                {
                                    { "partId", partId }
                                };
            var versionList = _repBomItem.ExecNamedSqlQuery<string>("GetBomVersion", parameters.ToArray()).ToList();
            var list = new List<object>();
            foreach (var version in versionList)
            {
                var x = new Dictionary<string, string>();
                x["version"] = version;
                list.Add(x);
            }

            return list;
        }

        public object GetBomData(dynamic request)
        {
            object ret;
            int partId = request.partid;
            var version = request.version;
            if (version == -1)
            {
                var parameters = new Dictionary<string, object>();
                parameters["partId"] = partId;
                ret = _repBomItem.ExecNamedSqlQuery<BomDataDTO>("GetBomData", parameters.ToArray()).ToList();
            }
            else
            {
                var parameters = new Dictionary<string, object>();
                parameters["partId"] = partId;
                parameters["version"] = (string)version;
                ret = _repBomItem.ExecNamedSqlQuery<BomDataDTO>("GetBomData2", parameters.ToArray()).ToList();
            }

            return ret;
        }

        public object GetPartInofo(dynamic request)
        {
            string itempartid = request.itempartid;
            string partid = request.partid;
            string version = request.version;

            var parameters = new Dictionary<string, object>();
            parameters["itempartid"] = itempartid;
            parameters["partid"] = partid;
            parameters["version"] = version;
            var ret = _repBomItem.ExecNamedSqlQuery<PartInofoDTO>("GetPartInofo", parameters.ToArray()).ToList();

            var partInofoList = new List<PartInofoDTO>();
            var item = ret.FirstOrDefault();
            if (item != null)
            {
                var partInofo = new PartInofoDTO();
                partInofo.Partname = item.Partname;
                partInofo.Itemcount = item.Itemcount;
                partInofo.Spec1 = item.Spec1;
                var locations = ret.Select(p => p.Location);
                string locationStr = string.Join(",", locations.ToArray()); // 位置获取S_BOM_LOCATION表中位置，以逗号分割
                partInofo.Location = locationStr;
                partInofoList.Add(partInofo);
            }

            return partInofoList;
        }

        public override void Add(SBopBaseDTO obj)
        {
            // CheckDuplication(obj);
            obj.Status = "0";
            var workflowid = Convert.ToInt32(obj.Workflowid);
            obj.Workflowlastversion = _repWorkflow.Get(workflowid).Version;
            base.Add(obj);
        }

        public object CallBopBaseSP(BopBaseRequestDTO request)
        {
            // 复制
            // editor: ""
            // from: oldBopBaseId
            // to: newBopBaseId

            // 发布
            // editor: ""
            // from: newBopBaseId
            // to: -1

            // 调用存储过程
            var spName = "MLT_BOP_BASE";
            var frombopbase = request.Frombopbase;
            var tobopbase = request.Tobopbase;
            var editor = request.Editor;
            RepositoryExtension.CallSP<SBopBase>(
            (Infrastructure.Hibernate.Data.Repository<SBopBase>)_repBopbase,
            spName,
            new SqlParameter("@frombopbase", frombopbase),
            new SqlParameter("@tobopbase", tobopbase),
            new SqlParameter("@editor", editor));

            return null;
        }

        public object CheckBeforePublish(int bopBaseId)
        {
            // 工艺清单发布时检查是否存在使用该清单所在工艺路线的RELEASE\WIP工单，
            var bopBaseDTO = FindById(bopBaseId);
            int wordkflowid = Convert.ToInt32(bopBaseDTO.Workflowid);

            // 如果存在，提示用户发布后这些工单将使用新配置，
            var status = new List<string>() { "RELEASE", "WIP" };
            var woBases = _repWoBase.Query()
                .Where(p => p.SWorkflow.Id == wordkflowid && status.Contains(p.SWoStatus.Name));
            if (woBases.Count() > 0)
            {
                var woList = woBases.Select(p => p.WorkOrder).ToList<string>();
                return woList;
            }

            return null;
        }

        // 工艺清单发布
        [Transactional]
        public object Publish(BopBaseRequestDTO request)
        {
            int oldBopBaseId = request.BopBaseId;
            string editor = request.Editor;
            List<string> workorders = request.Workorders;

            // 根据传入的BopBaseId 获取发布态的BopBaseId
            var oldBopBase = _repBopbase.Get(oldBopBaseId);
            int newBopBaseId;
            if (oldBopBase.SPart != null)
            {
                newBopBaseId = _repBopbase.Query().Where(p =>
                                p.SPart.Id == oldBopBase.SPart.Id &&
                                p.SWorkflow.Id == oldBopBase.SWorkflow.Id &&
                                p.WorkflowVersion == oldBopBase.WorkflowVersion &&
                                p.Status == "1").First().Id;
            }
            else
            {
                newBopBaseId = _repBopbase.Query().Where(p =>
                                p.SFamily.Id == oldBopBase.SFamily.Id &&
                                p.SWorkflow.Id == oldBopBase.SWorkflow.Id &&
                                p.WorkflowVersion == oldBopBase.WorkflowVersion &&
                                p.Status == "1").First().Id;
            }

            var newBopBase = _repBopbase.Get(newBopBaseId);
            // 新增 工艺清单历史表，保存已发布记录 S_BOP
            var bop = new SBop();
            if (newBopBase.SPart != null)
            {
                bop.PartId = newBopBase.SPart.Id;
            }

            bop.FamilyId = newBopBase.SFamily.Id;
            bop.WorkflowId = newBopBase.SWorkflow.Id;
            bop.WorkflowVersion = newBopBase.SWorkflow.Version;
            // json串保存工艺清单下维护的全部内容
            bop.Content = ExportBopContent(newBopBaseId);
            bop.EffectiveDate = DateTime.Now;
            bop.Editor = editor;
            _repBop.Add(bop);

            // 若传入了workorders, 更新WoBase
            if (workorders.Count > 0)
            {
                // 将RELEASE\WIP的工单的工艺路线更新为最新版、工艺清单为空
                UpdateWoBase(request);
            }

            return null;
        }

        private void UpdateWoBase(BopBaseRequestDTO request)
        {
            // 发布后将RELEASE\WIP的工单的工艺清单更新为最新版
            var workorders = request.Workorders;
            var editor = request.Editor;
            foreach (var wo in workorders)
            {
                // 获取旧WoBase
                WoBase old = _repWoBase.Query().Where(p => p.WorkOrder == wo).FirstOrDefault();
                // 工艺清单为空
                old.BopId = null;
                // 编辑人
                old.Editor = editor;
                // 更新
                _repWoBase.Update(old);
            }
        }

        // json串保存工艺清单下维护的全部内容
        public string ExportBopContent(int bopBaseId)
        {
            var content = new Dictionary<string, object>();

            // 1. S_BOP_BASE   工艺清单基础表
            content["bopBase"] = BopBase(bopBaseId);

            // 2. S_BOP_PROCESS_BOM    工艺清单-工序下物料清单表
            content["bopProcessBom"] = BopProcessBom(bopBaseId);

            // 3. S_BOP_PROCESS_LABEL  工艺清单-工序下标签设定表
            content["bopProcessLabel"] = BopProcessLabel(bopBaseId);

            // 4. S_BOP_PROCESS_SAMPLING 工艺清单-工序下抽检规则设定表
            content["bopProcessSampling"] = BopProcessSampling(bopBaseId);

            // 5. S_BOP_PROCESS_TESTITEM   工艺清单-工序下测试项设定表
            content["bopProcessTestitem"] = BopProcessTestitem(bopBaseId);

            // 6. S_BOP_SPEC   工艺清单-参数规格表
            content["bopSpec"] = BopSpec(bopBaseId);

            var jsonString = JsonConvert.SerializeObject(content);
            return jsonString;
        }

        private object BopBase(int bopBaseId)
        {
            // S_BOP_BASE   工艺清单基础表
            var x_bopBase = new Dictionary<string, object>();
            SBopBase bopBase = _repBopbase.Get(bopBaseId);
            x_bopBase["id"] = bopBase.Id;
            if (bopBase.SPart != null)
            {
                x_bopBase["partId"] = bopBase.SPart.Id;
            }
            else
            {
                x_bopBase["partId"] = null;
            }

            x_bopBase["familyId"] = bopBase.SFamily.Id;
            x_bopBase["workflowId"] = bopBase.SWorkflow.Id;
            x_bopBase["status"] = bopBase.Status;
            return x_bopBase;
        }

        private object BopProcessBom(int bopBaseId)
        {
            // S_BOP_PROCESS_BOM    工艺清单-工序下物料清单表
            var x_bopProcessBom = new List<object>();
            var bopProcessBoms = _repBopProcessBom.Query().Where(p => p.BopBase.Id == bopBaseId);
            foreach (var item in bopProcessBoms)
            {
                var bopProcessBom = new Dictionary<string, object>();
                bopProcessBom["id"] = item.Id;
                bopProcessBom["bopBaseId"] = item.BopBase.Id;
                bopProcessBom["processId"] = item.Process.Id;
                bopProcessBom["itemPartId"] = item.ItemPart.Id;
                bopProcessBom["qty"] = item.Qty;
                bopProcessBom["location"] = item.Location;
                x_bopProcessBom.Add(bopProcessBom);
            }

            return x_bopProcessBom;
        }

        private object BopProcessLabel(int bopBaseId)
        {
            // S_BOP_PROCESS_LABEL  工艺清单-工序下标签设定表
            var x_bopProcessLabel = new List<object>();
            var bopProcessLabels = _repBopProcessLabel.Query().Where(p => p.BopBaseId == bopBaseId);
            foreach (var item in bopProcessLabels)
            {
                var bopProcessLabel = new Dictionary<string, object>();
                bopProcessLabel["id"] = item.Id;
                bopProcessLabel["bopBaseId"] = item.BopBaseId;
                bopProcessLabel["processId"] = item.ProcessId;
                bopProcessLabel["specialCheck"] = item.SpecialCheck;
                bopProcessLabel["printTemplate"] = item.PrintTemplate.Id;
                bopProcessLabel["piece"] = item.Piece;
                bopProcessLabel["trigger"] = item.SSetting.Id;
                bopProcessLabel["priority"] = item.Priority;
                x_bopProcessLabel.Add(bopProcessLabel);
            }

            return x_bopProcessLabel;
        }

        private object BopProcessSampling(int bopBaseId)
        {
            // S_BOP_PROCESS_SAMPLING 工艺清单-工序下抽检规则设定表
            var x_bopProcessSampling = new List<object>();
            var bopProcessSamplings = _repBopProcessSampling.Query().Where(p => p.SBopBase.Id == bopBaseId);
            foreach (var item in bopProcessSamplings)
            {
                var bopProcessSampling = new Dictionary<string, object>();
                bopProcessSampling["id"] = item.Id;
                bopProcessSampling["bopBaseId"] = item.SBopBase.Id;
                bopProcessSampling["processId"] = item.ProcessId;
                bopProcessSampling["samplingPlanId"] = item.SSamplingPlan.Id;
                bopProcessSampling["samplingRuleId"] = item.SSamplingRule.Id;
                bopProcessSampling["oqcSamplingType"] = item.OqcSamplingType;
                bopProcessSampling["oqcSamplingQty"] = item.OqcSamplingQty;
                x_bopProcessSampling.Add(bopProcessSampling);
            }

            return x_bopProcessSampling;
        }

        private object BopProcessTestitem(int bopBaseId)
        {
            // S_BOP_PROCESS_TESTITEM   工艺清单-工序下测试项设定表
            var x_bopProcessTestitems = new List<object>();
            var bopProcessTestitems = _repBopProcessTestitem.Query().Where(p => p.BopBaseId == bopBaseId);
            foreach (var item in bopProcessTestitems)
            {
                var bopProcessTestitem = new Dictionary<string, object>();
                bopProcessTestitem["id"] = item.Id;
                bopProcessTestitem["bopBaseId"] = item.BopBaseId;
                bopProcessTestitem["testItemId"] = item.STestItem.Id;
                bopProcessTestitem["processId"] = item.ProcessId;
                bopProcessTestitem["sl"] = item.Sl;
                bopProcessTestitem["usl"] = item.Usl;
                bopProcessTestitem["lsl"] = item.Lsl;
                bopProcessTestitem["cl"] = item.Cl;
                bopProcessTestitem["ucl"] = item.Ucl;
                bopProcessTestitem["lcl"] = item.Lcl;
                bopProcessTestitem["sequence"] = item.Sequence;
                x_bopProcessTestitems.Add(bopProcessTestitem);
            }

            return x_bopProcessTestitems;
        }

        private object BopSpec(int bopBaseId)
        {
            // S_BOP_SPEC   工艺清单-参数规格表
            var x_bopSpecs = new List<object>();
            var bopSpecs = _repBopSpec.Query().Where(p => p.SBopBase.Id == bopBaseId);
            foreach (var item in bopSpecs)
            {
                var bopSpec = new Dictionary<string, object>();
                bopSpec["id"] = item.Id;
                bopSpec["bopBaseId"] = item.SBopBase.Id;
                bopSpec["paramKey"] = item.ParamKey;
                bopSpec["paramValue"] = item.ParamValue.Id;
                x_bopSpecs.Add(bopSpec);
            }

            return x_bopSpecs;
        }

        [Transactional]
        public void UpdateUdt(int id)
        {
            var bopBase = _repBopbase.Get(id);
            bopBase.Udt = DateTime.Now;
            _repBopbase.Update(bopBase);
        }

        [Transactional]
        public object GetKeyPart(int familyId)
        {
            var collection = _repPart.Query().Where(p => p.Keypart == true && p.SFamily.Id == familyId).ToList();
            var keyPartList = new List<BomDataDTO>();
            foreach (var item in collection)
            {
                var keyPart = new BomDataDTO()
                {
                    Part_id = item.Id.ToString(),
                    Part_name = item.PartName
                };

                keyPartList.Add(keyPart);
            }

            return keyPartList;
        }
    }
}
