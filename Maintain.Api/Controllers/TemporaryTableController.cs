using System.Collections.Generic;
using System.Data;
using System.Linq;
using IIMes.Infrastructure.BaseController;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Core.Model;
using IIMes.Services.Maintain.Interface;
using IIMes.Services.Maintain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using NPOI.XSSF.UserModel;

namespace IIMes.Services.Maintain.Controllers
{
    public class TemporaryTableController : BaseController<TemporaryTable, TemporaryTableDTO>
    {
        private readonly IRepository<TemporaryTable> _repTemporaryTable;

        public TemporaryTableController(
            ILogger<TemporaryTable> logger,
            ITemporaryTableService temporaryTableService,
            IRepository<TemporaryTable> repTemporaryTable)
        : base(logger, temporaryTableService)
        {
            _repTemporaryTable = repTemporaryTable;
        }

        [Route("upload")]
        [HttpPost]
        [DisableRequestSizeLimit]
        public virtual ActionResult PostForm()
        {
            var formFiles = Request.Form.Files;

            StringValues cachedata;
            var entityname = Request.Form.TryGetValue("entityname", out cachedata) == true ? cachedata.ToString() : "null";
            var editor = Request.Form.TryGetValue("editor", out cachedata) == true ? cachedata.ToString() : "null";

            var stream = formFiles[0].OpenReadStream();
            var xssfworkbook = new XSSFWorkbook(stream);
            // Excel To DataTable
            var dataTable = ExcelHelper.ExcelToTableForXLSX(xssfworkbook, "sheet1");
            // DataTable To IList<T>
            var objlist = new List<TemporaryTable>();
            foreach (DataRow dr in dataTable.Rows)
            {
                var obj = new TemporaryTable();
                var itemArray = dr.ItemArray;
                var dic = new Dictionary<string, object>();

                var number = 1;
                foreach (var item in itemArray)
                {
                    var name = $"C{number}";
                    dic[name] = itemArray[number - 1].ToString();
                    number++;
                }

                obj = ExcelHelper.DicToObject<TemporaryTable>(dic);
                obj.Entityname = entityname;
                obj.Status = "0";
                obj.Editor = editor;
                objlist.Add(obj);
            }

            _repTemporaryTable.Add(objlist);
            return Ok();
        }
    }
}
