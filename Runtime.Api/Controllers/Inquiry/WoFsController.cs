using System;
using System.Linq;
using IIMes.Infrastructure.BaseController;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Exception;
using IIMes.Infrastructure.Query;
using IIMes.Services.Runtime.Interface;
using IIMes.Services.Runtime.Interface.DTO;
using IIMes.Services.Runtime.Model.Product;
using IIMes.Services.Runtime.Model.Production;
using IIMes.Services.Runtime.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IIMes.Services.Runtime.Controllers
{
    public class WoFsController : BaseController
    {
        private readonly ILogger<WoFsController> _logger;

        private readonly IRepository<WoFs> _repWoFs;
        private readonly IRepository<FsStatus> _repFsStatus;

        public WoFsController(ILogger<WoFsController> logger, IRepository<WoFs> repWoFs, IRepository<FsStatus> repFsStatus)
        {
            _logger = logger;
            _repWoFs = repWoFs;
            _repFsStatus = repFsStatus;
        }

        // 检查
        [Route("QueryByWoAndEq")]
        [HttpGet]
        public ActionResult GetWoFs(string wo, int equipmentId)
        {
            var wofs = _repWoFs.GetWoFsByWoAndEq(wo, equipmentId).ToList();
            if (wofs.Count == 0)
            {
                // WO_FS不存在报错：请维护工单料站
                throw new BizException("CHK013");
            }

            var fsStatuses = _repFsStatus.Query().Where(p => p.WorkOrder == wo && p.EquipmentId == equipmentId).FirstOrDefault();
            if (fsStatuses != null && fsStatuses.Status == 1)
            {
                // FS_STATUS.status等于1“已确认上料”，报错：工单workorderno不需要上料
                throw new BizException("CHK014", wo);
            }

            return Ok();
        }
    }
}