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
    public class PartController : BaseController<SPart, SPartDTO>
    {
        private readonly IRepository<SFamily> _repSFamily;
        private readonly IRepository<SSetting> _repSSetting;
        private readonly IRepository<SVendor> _repVendor;
        private readonly IRepository<SCustomer> _repCustomer;

        public PartController(
            ILogger<SPart> logger,
            IPartService partService,
            IRepository<SFamily> repSFamily,
            IRepository<SSetting> repSSetting,
            IRepository<SVendor> repVendor,
            IRepository<SCustomer> repCustomer)
        : base(logger, partService)
        {
            _repSFamily = repSFamily;
            _repSSetting = repSSetting;
            _repVendor = repVendor;
            _repCustomer = repCustomer;
        }

        [Route("{upload}")]
        [HttpPost]
        [DisableRequestSizeLimit]
        public virtual ActionResult PostForm()
        {
            var formFiles = Request.Form.Files;
            var stream = formFiles[0].OpenReadStream();
            var xssfworkbook = new XSSFWorkbook(stream);
            // Excel To DataTable
            var dataTable = ExcelHelper.ExcelToTableForXLSX(xssfworkbook, "part");
            // DataTable To IList<T>
            var list = new List<ExcelPartDTO>();
            foreach (DataRow dr in dataTable.Rows)
            {
                var excelPart = new ExcelPartDTO();
                var itemArray = dr.ItemArray;
                excelPart.PartNo = itemArray[0].ToString();
                excelPart.PartName = itemArray[1].ToString();
                excelPart.Spec1 = itemArray[2].ToString();
                excelPart.Spec2 = itemArray[3].ToString();
                excelPart.Version = itemArray[4].ToString();
                excelPart.ModelCode = itemArray[5].ToString();
                excelPart.PartXl = itemArray[6].ToString();
                excelPart.MaterialTypeName = itemArray[7].ToString();
                excelPart.Uom = itemArray[8].ToString();
                excelPart.MatchRule = itemArray[9].ToString();
                excelPart.UniqueCheck = itemArray[10].ToString();
                excelPart.VendorCode = itemArray[11].ToString();
                excelPart.VendorPartNo = itemArray[12].ToString();
                excelPart.CustomerCode = itemArray[13].ToString();
                excelPart.CustomerPartNo = itemArray[14].ToString();
                list.Add(excelPart);
            }

            foreach (var item in list)
            {
                // ExcelPartDTO To SPartDTO
                var obj = new SPartDTO();
                obj.PartNo = item.PartNo;
                obj.PartName = item.PartName;
                obj.Spec1 = item.Spec1;
                obj.Spec2 = item.Spec2;
                obj.Version = item.Version;
                obj.PartXl = item.PartXl;
                obj.Uom = item.Uom;
                obj.MatchRule = item.MatchRule;
                obj.UniqueCheck = item.UniqueCheck == "1" ? true : false;
                obj.VendorPartNo = item.VendorPartNo;
                obj.CustomerPartNo = item.CustomerPartNo;

                var model = _repSFamily.Query().Where(p => p.Code == item.ModelCode).First();
                obj.Model = new LayerCommonDTO() { Id = model.Id };
                var setting = _repSSetting.Query().Where(p => p.Name == item.MaterialTypeName).First();
                obj.MaterialType = new LayerCommonDTO() { Id = setting.Id };
                var vendor = _repVendor.Query().Where(p => p.Code == item.VendorCode).First();
                obj.Vendor = new LayerCommonDTO() { Id = vendor.Id };
                var customer = _repCustomer.Query().Where(p => p.Code == item.CustomerCode).First();
                obj.Customer = new LayerCommonDTO() { Id = customer.Id };
                obj.Editor = "admin";
                // 默认关键料为0
                obj.Keypart = false;
                // 解析后存入数据库
                Service.Add(obj);
            }

            return Ok();
        }
    }
}