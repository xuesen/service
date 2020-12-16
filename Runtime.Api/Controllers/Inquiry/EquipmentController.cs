using System;
using IIMes.Infrastructure.BaseController;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Query;
using IIMes.Services.Core.Model;
using IIMes.Services.Runtime.Interface;
using IIMes.Services.Runtime.Interface.DTO;
using IIMes.Services.Runtime.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IIMes.Services.Runtime.Controllers
{
    public class EquipmentController : BaseController
    {
        private readonly ILogger<EquipmentController> _logger;
        private readonly IEquipmentService _equipmentService;

        public EquipmentController(ILogger<EquipmentController> logger, IEquipmentService equipmentService)
        {
            _logger = logger;
            _equipmentService = equipmentService;
        }

        // 获取设备
        [Route("{equipmentcode}")]
        [HttpGet]
        public ActionResult GetEquipment([FromRoute]string equipmentcode)
        {
            var equipmentObj = _equipmentService.GetEquipment(equipmentcode);
            return Ok(equipmentObj);
        }
    }
}