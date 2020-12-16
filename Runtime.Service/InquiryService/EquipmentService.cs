using System.Collections.Generic;
using AutoMapper;
using IIMes.Infrastructure.Aspect;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Exception;
using IIMes.Infrastructure.Service;
using IIMes.Services.Runtime.Interface;
using IIMes.Services.Runtime.Interface.DTO;
using IIMes.Services.Runtime.Repository;
using IIMes.Services.Runtime.Model.Resource;

namespace IIMes.Services.Runtime.Services
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IRepository<Equipment> _repEquipment;

        public EquipmentService(IRepository<Equipment> repEquipment)
        {
            _repEquipment = repEquipment;
        }

        public CommonDTO GetEquipment(string equipmentcode)
        {
            CommonDTO equipmentDTO = new CommonDTO();
            var equipment = _repEquipment.GetEquipment(equipmentcode);
            if (equipment == null)
            {
                // 设备不存在
                // throw new Exception("设备不存在");
                throw new BizException("CHK005");
            }
            else
            {
                equipmentDTO.Id = equipment.Id;
                equipmentDTO.Code = equipment.Code;
                equipmentDTO.Name = equipment.Name;
            }

            return equipmentDTO;
        }
    }
}
