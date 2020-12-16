using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AutoMapper;
using IIMes.Infrastructure.BaseController;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Query;
using IIMes.Services.Core.Model;
using IIMes.Services.Maintain.Interface;
using IIMes.Services.Maintain.Repository;
using IIMes.Services.Maintain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NPOI.XSSF.UserModel;

namespace IIMes.Services.Maintain.Controllers
{
    public class WorkflowController : BaseController<SWorkflow, SWorkflowDTO>
    {
        private readonly IRepository<SWorkflow> _repWorkflow;
        private readonly IRepository<SSetting> _repSetting;
        private readonly IRepository<SWorkflowBase> _repWorkflowBase;
        private readonly IRepository<SPart> _repPart;
        private readonly IRepository<SFamily> _repFamily;

        private readonly IMapper _mapper;
        private readonly IWorkflowService _workflowService;

        public WorkflowController(
            IMapper mapper,
            ILogger<SWorkflow> logger,
            IWorkflowService workflowService,
            IRepository<SWorkflow> repWorkflow,
            IRepository<SSetting> repSetting,
            IRepository<SWorkflowBase> repWorkflowBase,
            IRepository<SFamily> repFamily,
            IRepository<SPart> repPart)
        : base(logger, workflowService)
        {
            _repWorkflow = repWorkflow;
            _repSetting = repSetting;
            _repWorkflowBase = repWorkflowBase;
            _repPart = repPart;
            _repFamily = repFamily;
            _mapper = mapper;
            _workflowService = workflowService;
        }

        // 工艺路线左侧显示列表
        [Route("workflowlist")]
        [HttpPost]
        public virtual ActionResult WorkFlowList()
        {
            var data = new List<WorkFlowDisplayDTO>();

            var workflowBaseIds = _repWorkflow.Query().Select(x => x.SWorkflowBase.Id).Distinct();

            // 获取存在workflow的workflowBase
            var resWorkflowBase = _repWorkflowBase.Query().Where(x => workflowBaseIds.Contains(x.Id));
            foreach (var workflowBase in resWorkflowBase)
            {
                var display = new WorkFlowDisplayDTO();
                display.Id = workflowBase.Id;
                display.Name = workflowBase.Name;
                // Part
                if (workflowBase.SPart != null)
                {
                    var resPart = _repPart.Query().Where(p => p.Id == workflowBase.SPart.Id).FirstOrDefault();
                    display.Part = _mapper.Map<SPart, SPartDTO>(resPart);
                }

                // Family
                if (workflowBase.SFamily != null)
                {
                    var resFamily = _repFamily.Query().Where(p => p.Id == workflowBase.SFamily.Id).FirstOrDefault();
                    display.Family = _mapper.Map<SFamily, FamilyDTO>(resFamily);
                }

                // Workflows 只带最新版工艺路线S_WORKFLOW. VERSION_SEQUENCE，只有编辑态和发布态
                // 编辑态0
                var resWorkflow0 = _repWorkflow.Query()
                    .Where(p => p.SWorkflowBase.Id == workflowBase.Id && p.Status == "0")
                    .OrderByDescending(p => Convert.ToInt32(p.VersionSequence)).FirstOrDefault();
                // 发布态1
                var resWorkflow1 = _repWorkflow.Query()
                    .Where(p => p.SWorkflowBase.Id == workflowBase.Id && p.Status == "1")
                    .OrderByDescending(p => Convert.ToInt32(p.VersionSequence)).FirstOrDefault();
                var xxx = new List<SWorkflow>();
                xxx.Add(resWorkflow0);
                xxx.Add(resWorkflow1);
                var workflows = new List<SWorkflowDTO>();
                foreach (var item in xxx)
                {
                    if (item != null)
                    {
                        var sWorkflowDTO = _mapper.Map<SWorkflow, SWorkflowDTO>(item);
                        workflows.Add(sWorkflowDTO);
                    }
                }

                display.Workflows = workflows;
                data.Add(display);
            }

            var list = data.OrderBy(x => x.Name);
            return Ok(list);
        }

        // 得到工艺流程图
        [Route("getDiagramById/{id}")]
        [HttpPost]
        public virtual ActionResult GetDiagramById([FromRoute] int id)
        {
            var x = Service.FindById(id);
            var workflowjson = JsonConvert.DeserializeObject<object>(x.WorkflowJson);
            return Ok(workflowjson);
        }

        // 新增 更新工艺路线
        [Route("saveWorkflow")]
        [HttpPost]
        public virtual ActionResult SaveWorkflow([FromBody] dynamic request)
        {
            var ret = _workflowService.SaveWorkflow(request);
            return Ok(ret);
        }

        // 删除Workflow以及WorkflowStep
        [Route("deleteByWorkflowId/{id}")]
        [HttpPost]
        public virtual ActionResult DeleteWorkflowAndWorkflowStep([FromRoute] int id)
        {
            _workflowService.DeleteWorkflowAndWorkflowStep(id);
            return Ok();
        }

        // 复制用显示列表
        [Route("copylist")]
        [HttpPost]
        public virtual ActionResult CopyList()
        {
            var ret = _workflowService.CopyList();

            return Ok(ret);
        }

        // 发布前检查
        [Route("checkBeforePublish")]
        [HttpPost]
        public virtual ActionResult CheckBeforePublish(SWorkflowBaseDTO request)
        {
            var ret = _workflowService.CheckBeforePublish(request);
            return Ok(ret);
        }

        // 发布
        [Route("Publish")]
        [HttpPost]
        public virtual ActionResult Publish(WorkflowRequestDTO request)
        {
            _workflowService.Publish(request);
            return Ok();
        }

        // 新增前检查
        [Route("Check")]
        [HttpPost]
        public virtual ActionResult Check(dynamic request)
        {
            var result = _workflowService.CheckBeforeAdd(request);
            return Ok(result);
        }

        // 根据workflow_id得到workflow_step
        [Route("workflowstep/{workflowid}")]
        [HttpGet]
        public virtual ActionResult GetProcessList([FromRoute] int workflowid)
        {
            var processlst = _workflowService.GetProcessList(workflowid).ToList();
            return Ok(processlst);
        }
    }
}