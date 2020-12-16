using System.Collections.Generic;
using AutoMapper;
using IIMes.Infrastructure.Aspect;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Exception;
using IIMes.Infrastructure.Service;
using IIMes.Services.Runtime.Interface;
using IIMes.Services.Runtime.Interface.DTO;
using IIMes.Services.Runtime.Model.Product;
using IIMes.Services.Runtime.Repository;

namespace IIMes.Services.Runtime.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IRepository<Material> _repMaterial;
        public MaterialService(IRepository<Material> repMaterial)
        {
            _repMaterial = repMaterial;
        }

        public MaterialDTO GetPartByReel(string inputno)
        {
            var material = _repMaterial.GetPartByReel(inputno);
            var materialDTO = new MaterialDTO();
            if (material != null)
            {
                materialDTO.ReelId = material.ReelId;
                materialDTO.Pn = material.Pn;

                if (material.LeftAmount <= 0)
                {
                    // 当前Reel剩余数为0
                    throw new BizException("CHK040", materialDTO.ReelId);
                }
            }

            return materialDTO;
        }

        public MaterialDTO GetPartByPart(string inputno)
        {
            var materialDTO = new MaterialDTO();
            var material = _repMaterial.GetPartByPart(inputno);
            if (material != null)
            {
                materialDTO.ReelId = material.ReelId;
                materialDTO.Pn = material.Pn;

                if (material.LeftAmount <= 0)
                {
                    // 当前Reel剩余数为0
                    throw new BizException("CHK040", materialDTO.ReelId);
                }
            }
            else
            {
                // ReelId/PN不存在
                throw new BizException("CHK008");
            }

            return materialDTO;
        }
    }
}