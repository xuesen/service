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

namespace IIMes.Services.Runtime.Services
{
    public class WORScrapService : IWORScrapService
    {
        private readonly IRepository<WorScrap> _repWorScrap;
        private readonly IRepository<ScrapReason> _repScrapReason;
        private readonly IRepository<WorLog> _repWorLog;
        private readonly IRepository<WoBase> _repWoBase;
        private readonly IRepository<WoStatus> _repWoStatus;

        public WORScrapService(IRepository<WorScrap> repWorScrap, IRepository<ScrapReason> repScrapReason, IRepository<WoStatus> repWoStatus, IRepository<WorLog> repWorLog, IRepository<WoBase> repWoBase)
        {
            _repWorScrap = repWorScrap;
            _repScrapReason = repScrapReason;
            _repWoStatus = repWoStatus;
            _repWorLog = repWorLog;
            _repWoBase = repWoBase;
            
        }

        // 获取报废原因
        public object QueryScrapReason()
        {
            var ret = _repScrapReason.Query();
            return ret;
        }

        // 保存报废信息
        [Transactional]
        public int SaveWorScrap(WorScrapRequestDTO request)
        {
            var ret = 0;
            // 插入WOR_SCRAP
            AddWorScrap(request);

            // 更新WO_BASE
            UpdateWoBase(request);

            return ret;
        }

        private void AddWorScrap(WorScrapRequestDTO request)
        {
            // AddWorScrap
            var worScrap = new WorScrap(){
                WorkOrder = request.WorkOrder,
                Qty = request.Qty,
                ScrapCode = request.ScrapReason,
                Editor = request.Editor
            };
            _repWorScrap.Add(worScrap);
        }

        private void UpdateWoBase(WorScrapRequestDTO request)
        {
            // UpdateWoBase
            var wo = request.WorkOrder;
            var thisScrapQty = request.Qty;
            var old = _repWoBase.GetWoBaseByWo(wo);
            // WO_BASE.SCRAP_QTY+本次报废数量
            old.ScrapQty = old.ScrapQty + thisScrapQty;
            // 检查WO_BASE.SCRAP_QTY+OUTPUT_QTY=INPUT_QTY时，更新WO_STATUS_ID、WO_CLOSE_TIME
            var x = old.ScrapQty + old.OutputQty == old.InputQty ? true : false;
            if (x)
            {
                var WoStatusId = _repWoStatus.Query().Where(p => p.Name == "COMPLETE").First().Id;
                old.WoStatus = new WoStatus(){Id = WoStatusId};
                old.WoCloseTime = DateTime.Now;
            }

            _repWoBase.Update(old);
        }
    }
}