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
using System.Data;
using System.IO;
using System.Linq;
using AutoMapper;
using IIMes.Infrastructure.Aspect;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Service;
using IIMes.Services.Core.Model;
using IIMes.Services.Maintain.Interface;
using IIMes.Services.Maintain.Repository;
using NPOI.XSSF.UserModel;

namespace IIMes.Services.Maintain.Services
{
    public partial class BomService : BaseMaintainService<SBom, SBomDTO>, IBomService
    {
        private readonly IRepository<SBom> _repSBom;
        private readonly IRepository<SBomBase> _repSBomBase;
        private readonly IRepository<SBomItem> _repSBomItem;
        private readonly IRepository<SBomLocation> _repSBomLocation;
        private readonly IRepository<SPart> _repSPart;

        public BomService(
            IRepository<SBomBase> repSBomBase,
            IRepository<SBomItem> repSBomItem,
            IRepository<SBomLocation> repSBomLocation,
            IRepository<SPart> repSPart,
            IRepository<SBom> repSBom,
            IMapper mapper)
        : base(repSBom, mapper)
        {
            _repSBom = repSBom;
            _repSBomBase = repSBomBase;
            _repSBomItem = repSBomItem;
            _repSBomLocation = repSBomLocation;
            _repSPart = repSPart;
        }

        [Transactional]
        public object ImportBomExcel(Stream stream)
        {
            var xssfworkbook = new XSSFWorkbook(stream);
            // Excel To DataTable
            var dataTable = ExcelHelper.ExcelToTableForXLSX(xssfworkbook, "bom");
            // DataTable To IList<T>
            var list = new List<ExcelBomDTO>();
            foreach (DataRow dr in dataTable.Rows)
            {
                var excelBom = new ExcelBomDTO();
                var itemArray = dr.ItemArray;
                excelBom.PartNo = itemArray[0].ToString();
                excelBom.Version = itemArray[1].ToString();
                excelBom.ItemPartNo = itemArray[2].ToString();
                excelBom.ItemVersion = itemArray[3].ToString();
                excelBom.ItemCount = itemArray[4].ToString();
                excelBom.ItemGroup = itemArray[5].ToString();
                excelBom.PrimaryKey = itemArray[6].ToString();
                excelBom.Location = itemArray[7].ToString();
                list.Add(excelBom);
            }

            foreach (var item in list)
            {
                var part = _repSPart.Query().Where(p => p.PartNo == item.PartNo).First();
                var itemPart = _repSPart.Query().Where(p => p.PartNo == item.ItemPartNo).First();

                object bomBaseId;
                object bomId;

                // 1.查询BomBase是否已存在记录
                var resSBomBase = _repSBomBase.Query().Where(p => p.SPart.Id == part.Id).FirstOrDefault();
                if (resSBomBase != null)
                {
                    // 更新S_BOM_BASE.VERSION
                    resSBomBase.LastVersion = item.Version;
                    resSBomBase.Udt = DateTime.Now;
                    _repSBomBase.Update(resSBomBase);
                    // 获取S_BOM_BASE.Id
                    bomBaseId = resSBomBase.Id;
                }
                else
                {
                    // 新增S_BOM_BASE
                    var bomBase = new SBomBase();
                    bomBase.SPart = new SPart() { Id = part.Id };
                    bomBase.Name = part.PartName;
                    bomBase.LastVersion = item.Version;
                    bomBase.Editor = "admin";
                    bomBaseId = _repSBomBase.Add(bomBase);
                }

                // 2.查询Bom是否已存在记录
                var resSBom = _repSBom.Query().Where(p => p.SBomBase.Id == (int)bomBaseId && p.Version == item.Version).FirstOrDefault();
                if (resSBom != null)
                {
                    // 获取S_BOM.Id
                    bomId = resSBom.Id;
                }
                else
                {
                    // 新增S_BOM
                    var sBom = new SBom();
                    sBom.SBomBase = new SBomBase() { Id = (int)bomBaseId };
                    sBom.Version = item.Version;
                    sBom.Status = "1";
                    sBom.Description = "";
                    sBom.Editor = "admin";
                    bomId = _repSBom.Add(sBom);
                }

                // 3. 新增S_BOM_ITEM
                var sBomItem = new SBomItem();
                sBomItem.SBom = new SBom() { Id = (int)bomId };
                sBomItem.SPart = new SPart() { Id = itemPart.Id };
                sBomItem.ItemCount = Convert.ToDecimal(item.ItemCount);
                sBomItem.ItemGroup = item.ItemGroup;
                sBomItem.ItemVersion = item.ItemVersion;
                sBomItem.PrimaryKey = item.PrimaryKey;
                sBomItem.Editor = "admin";
                var bomItemId = _repSBomItem.Add(sBomItem);
                // 4. 新增S_BOM_LOCATION（多个）
                var locationArray = item.Location.Split(',');
                foreach (var location in locationArray)
                {
                    var sBomLocation = new SBomLocation();
                    sBomLocation.BomItemId = (int)bomItemId;
                    sBomLocation.Location = location;
                    _repSBomLocation.Add(sBomLocation);
                }
            }

            return list;
        }
    }
}
