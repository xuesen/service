using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Threading;
using IIMes.Infrastructure.Exception;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace IIMes.Services.Maintain.Services
{
    public static class ExcelHelper
    {
        // excel文件流转化成datatable。
        public static DataTable ExcelToTableForXLSX(XSSFWorkbook xssfworkbook, string sheetName, bool haveNote = false)
        {
            int sheetCount = xssfworkbook.NumberOfSheets; // 获取表的数量
            string[] sheetNamelist = new string[sheetCount]; // 保存表的名称
            for (int i = 0; i < sheetCount; i++)
            {
                sheetNamelist[i] = xssfworkbook.GetSheetName(i);
            }

            var dt = new DataTable();
            // 默认第一个sheet
            var sheet = xssfworkbook.GetSheet(sheetNamelist[0]);
            // 表头 判断是否包含备注
            var firstRowNum = sheet.FirstRowNum;
            if (haveNote)
            {
                firstRowNum += 1;
            }

            var header = sheet.GetRow(firstRowNum);
            var columns = new List<int>();

            for (var i = 0; i < header.LastCellNum; i++)
            {
                var obj = GetValueTypeForXLSX(header.GetCell(i) as XSSFCell);
                if (obj == null || obj.ToString() == string.Empty)
                {
                    dt.Columns.Add(new DataColumn("Columns" + i.ToString()));
                    // continue;
                }
                else
                {
                    dt.Columns.Add(new DataColumn(obj.ToString()));
                }

                columns.Add(i);
            }

            // 数据
            for (var i = firstRowNum + 1; i <= sheet.LastRowNum; i++)
            {
                var dr = dt.NewRow();
                var hasValue = false;
                if (sheet.GetRow(i) == null)
                {
                    continue;
                }

                foreach (var j in columns)
                {
                    var cell = sheet.GetRow(i).GetCell(j);
                    if (cell != null && cell.CellType == CellType.Numeric)
                    {
                        // NPOI中数字和日期都是NUMERIC类型的，这里对其进行判断是否是日期类型
                        if (DateUtil.IsCellDateFormatted(cell))
                        {
                            dr[j] = cell.DateCellValue;
                        }
                        else
                        {
                            dr[j] = cell.NumericCellValue;
                        }
                    }
                    else
                    {
                        dr[j] = GetValueTypeForXLSX(sheet.GetRow(i).GetCell(j) as XSSFCell);
                    }

                    if (dr[j] != null && dr[j].ToString() != string.Empty)
                    {
                        hasValue = true;
                    }
                }

                if (hasValue)
                {
                    dt.Rows.Add(dr);
                }
                else
                {
                    return dt;
                }
            }

            return dt;
        }

        // 获取单元格类型(xlsx)
        private static object GetValueTypeForXLSX(XSSFCell cell)
        {
            if (cell == null)
                return null;
            switch (cell.CellType)
            {
                case CellType.Blank: // BLANK:
                    return null;
                case CellType.Boolean: // BOOLEAN:
                    return cell.BooleanCellValue;
                case CellType.Numeric: // NUMERIC:
                    return cell.NumericCellValue;
                case CellType.String: // STRING:
                    return cell.StringCellValue;
                case CellType.Error: // ERROR:
                    return cell.ErrorCellValue;
                case CellType.Formula: // FORMULA:
                default:
                    return "=" + cell.CellFormula;
            }
        }

        // Convert a List{T} to a DataTable.
        public static DataTable ToDataTable<T>(this List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                Type t = GetCoreType(prop.PropertyType);
                tb.Columns.Add(prop.Name, t);
            }

            foreach (T item in items)
            {
                var values = new object[props.Length];

                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }

        // 如果类型可空，则返回基础类型，否则返回类型
        public static Type GetCoreType(Type t)
        {
            if (t != null && IsNullable(t))
            {
                if (!t.IsValueType)
                {
                    return t;
                }
                else
                {
                    return Nullable.GetUnderlyingType(t);
                }
            }
            else
            {
                return t;
            }
        }

        // Determine of specified type is nullable
        public static bool IsNullable(Type t)
        {
            return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        // 是否为空行
        public static Boolean IsRowEmpty(IRow row, int count)
        {
            for (int i = 0; i < count; i++)
            {
                var cell = row.GetCell(i);
                if (cell != null && cell.CellType != CellType.Blank && !string.IsNullOrEmpty(cell.ToString().Trim()))
                {
                    return false;
                }
            }

            return true;
        }

        // 字典转对象
        public static T DicToObject<T>(Dictionary<string, object> dic)
        where T : new()
        {
            var md = new T();
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            foreach (var d in dic)
            {
                var filed = textInfo.ToTitleCase(d.Key);
                try
                {
                    var value = d.Value;
                    md.GetType().GetProperty(filed).SetValue(md, value);
                }
                catch (Exception e)
                {
                    throw new BizException(e.ToString());
                }
            }

            return md;
        }
    }
}