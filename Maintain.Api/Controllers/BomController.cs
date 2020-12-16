using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using IIMes.Infrastructure.BaseController;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Query;
using IIMes.Services.Core.Model;
using IIMes.Services.Maintain.Interface;
using IIMes.Services.Maintain.Repository;
using IIMes.Services.Maintain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NPOI.XSSF.UserModel;

namespace IIMes.Services.Maintain.Controllers
{
    public class BomController : BaseController<SBom, SBomDTO>
    {
        private readonly IRepository<SBom> _repSBom;
        private readonly IRepository<SBomBase> _repSBomBase;
        private readonly IRepository<SBomItem> _repSBomItem;
        private readonly IRepository<SBomLocation> _repSBomLocation;
        private readonly IRepository<SPart> _repSPart;
        private readonly IBomService _bomService;

        public BomController(
            ILogger<SBom> logger,
            IBomService bomService,
            IRepository<SBom> repSBom,
            IRepository<SBomBase> repSBomBase,
            IRepository<SBomItem> repSBomItem,
            IRepository<SBomLocation> repSBomLocation,
            IRepository<SPart> repSPart)
        : base(logger, bomService)
        {
            _repSBom = repSBom;
            _repSBomBase = repSBomBase;
            _repSBomItem = repSBomItem;
            _repSBomLocation = repSBomLocation;
            _repSPart = repSPart;
            _bomService = bomService;
        }

        // 上传Excel
        [Route("upload")]
        [HttpPost]
        [DisableRequestSizeLimit]
        public virtual ActionResult PostForm()
        {
            var formFiles = Request.Form.Files;
            var stream = formFiles[0].OpenReadStream();
            var result = _bomService.ImportBomExcel(stream);
            return Ok(result);
        }

        // 物料清单
        [Route("bomlist")]
        [HttpPost]
        public virtual ActionResult BomList()
        {
            var list = new List<BomListDTO>();
            // 只获取自产类型的
            // var resSPart = _repSPart.Query().Where(p => p.SSetting.Id == 29);

            // 物料清单显示的是S_BOM_BASE.PART_ID,不是全部
            var resSPart = _repSBomBase.Query().Select(p => p.SPart);

            foreach (var part in resSPart)
            {
                var bom = new BomListDTO();
                bom.Id = part.Id;
                bom.Name = part.PartName;
                var resSBomBase = _repSBomBase.Query().Where(p => p.SPart.Id == part.Id);
                var bomBaseList = new List<LayerBomsDTO>();
                foreach (var item in resSBomBase)
                {
                    var bomBase = new LayerBomsDTO();
                    bomBase.Id = item.Id;
                    bomBase.LastVersion = item.LastVersion;
                    bomBaseList.Add(bomBase);
                }

                bom.Boms = bomBaseList;
                list.Add(bom);
            }

            var result = list.OrderBy(x => x.Name);
            return Ok(result);
        }

        // 物料清单详情
        [Route("listDetail/{bomBaseId}")]
        [HttpPost]
        public virtual ActionResult BomListDetail([FromRoute] int bomBaseId)
        {
            var version = _repSBomBase.Get(bomBaseId).LastVersion;
            var bom = _repSBom.Query().Where(p => p.SBomBase.Id == bomBaseId && p.Version == version).First();

            var x = new BomListDetailDTO();
            x.Id = bom.Id;
            x.Version = bom.Version;
            x.Status = bom.Status;
            x.Editor = bom.Editor;

            var bomItems = new List<LayerBomItemsDTO>();

            var repSBomItem = _repSBomItem.Query().Where(p => p.SBom.Id == bom.Id);
            foreach (var sbomItem in repSBomItem)
            {
                var bomItemDTO = new LayerBomItemsDTO();
                bomItemDTO.Id = sbomItem.Id;
                bomItemDTO.ItemCount = sbomItem.ItemCount;
                bomItemDTO.ItemGroup = sbomItem.ItemGroup;
                bomItemDTO.ItemVersion = sbomItem.ItemVersion;
                bomItemDTO.PrimaryKey = sbomItem.PrimaryKey;
                var part = _repSPart.Query().Where(p => p.Id == sbomItem.SPart.Id).First();
                bomItemDTO.Part = new SPartDTO { Id = part.Id, PartNo = part.PartNo, PartName = part.PartName, Uom = part.Uom, Spec1 = part.Spec1 };

                var locations = new List<SBomLocation>();
                var resSBomLocation = _repSBomLocation.Query().Where(p => p.BomItemId == sbomItem.Id);
                foreach (var location in resSBomLocation)
                {
                    locations.Add(location);
                }

                bomItemDTO.Locations = locations;
                bomItems.Add(bomItemDTO);
            }

            x.BomItems = bomItems;
            return Ok(x);
        }
    }
}