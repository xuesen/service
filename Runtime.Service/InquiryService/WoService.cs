using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using IIMes.Infrastructure.Aspect;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Exception;
using IIMes.Infrastructure.Service;
using IIMes.Services.Runtime.Model;
using IIMes.Services.Runtime.Model.Production;
using IIMes.Services.Runtime.Interface;
using IIMes.Services.Runtime.Interface.DTO;
using IIMes.Services.Runtime.Repository;
using IIMes.Services.Runtime.Model.Process;
using IIMes.Infrastructure.Hibernate.Data;
using System;

namespace IIMes.Services.Runtime.Services
{
    public class WoService : IWoService
    {
        private readonly IRepository<WoBase> _repWobase;
        private readonly IRepository<WoStatus> _repWoStatus;
        // private readonly BopRepository _repBop;

        public WoService(
                        // BopRepository repBop,
                        IRepository<WoBase> repWobase,
                        IRepository<WoStatus> repWoStatus)
        {
            _repWobase = repWobase;
            _repWoStatus = repWoStatus;
            // _repBop = repBop;
        }

        public WoDTO GetWo(string workorder, string statusname)
        {
            // var bop = _repBop.GetBopByWo(workorder);
            WoDTO wodto = new WoDTO();
            // SMT贴片机上料允许WIP和RELEASE两种状态的工单上料 2020.10.15
            List<string> statusnamelst = new List<string>(statusname.Split(','));
            if (statusnamelst.Count > 0)
            {
                var flag = false;
                WoBase wobase = new WoBase();
                for (int i = 0; i < statusnamelst.Count; i++)
                {
                    var statusid = _repWoStatus.GetStatusidByName(statusnamelst[i]).Id;
                    wobase = _repWobase.GetWoBaseByWoStatus(workorder, statusid);
                    if (wobase != null)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag == false)
                {
                    // 该状态工单不存在
                    throw new BizException("CHK039");
                }
                else
                {
                    if (wobase.Workflow == null)
                    {
                        throw new BizException("CHK034");
                    }
                    var scrapqty = 0;
                    if (wobase.ScrapQty != null)
                    {
                        scrapqty = (int)wobase.ScrapQty;
                    }
                    wodto.WorkOrder = wobase.WorkOrder;
                    wodto.TargetQty = (int)wobase.TargetQty;
                    wodto.PartId = wobase.Part.Id;
                    wodto.PartNo = wobase.Part.PartNo;
                    wodto.PartName = wobase.Part.PartName;
                    wodto.Sequence = (int)wobase.Sequence;
                    wodto.ScrapQty = scrapqty;
                    wodto.WorkflowId = wobase.Workflow.Id;
                }
            }

            return wodto;
        }

        public IList<WoDTO> Query(WoRequestDTO wostatus)
        {
            IList<WoDTO> wodtolst = new List<WoDTO>();
            var wobaselst = _repWobase.QueryWos(wostatus.WoStatus).ToList();
            foreach (var item in wobaselst)
            {
                var scrapqty = 0;
                if (item.ScrapQty != null)
                {
                    scrapqty = (int)item.ScrapQty;
                }                
                WoDTO wodto = new WoDTO
                {
                    WorkOrder = item.WorkOrder,
                    TargetQty = (int)item.TargetQty,
                    PartId = item.Part.Id,
                    PartNo = item.Part.PartNo,
                    PartName = item.Part.PartName,
                    Sequence = item.Sequence,
                    WoScheduleTime = (DateTime)item.WoScheduleTime,
                    ScrapQty = scrapqty,
                    WorkflowId = item.Workflow == null ? 0 : item.Workflow.Id
                };
                wodtolst.Add(wodto);
            }
            return wodtolst;
        }
    }
}