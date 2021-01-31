using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections;
using System.Data;
using System.IO;

namespace AdunTech.Excel
{
    class ImportHelper
    {
        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static DataTable Read(ImportOptions options)
        {
            if (!options.IsExcleFile)
            {
                return null;
            }
            ISheet sheet = GetExcelSheet(options.Ext, options.File);
            if (sheet == null || sheet.LastRowNum < options.ColumnNameRowStartIndex)
            {
                return null;
            }

            DataTable dt = new DataTable();
            //列头
            foreach (ICell item in sheet.GetRow(options.ColumnNameRowStartIndex).Cells)
            {
                dt.Columns.Add(item.ToString(), typeof(string));
            }
            //写入内容
            IEnumerator rows = sheet.GetRowEnumerator();
            while (rows.MoveNext())
            {
                IRow row = GetExcelRow(options.Ext, rows);
                if (row.RowNum <= options.ColumnNameRowStartIndex)
                {
                    continue;
                }
                DataRow dr = dt.NewRow();
                ReadDataFromExcelRow(row, dr);
                dt.Rows.Add(dr);
            }
            return dt;
        }

        /// <summary>
        /// 获取Sheet
        /// </summary>
        /// <param name="ext">文件后缀</param>
        /// <param name="fs">文件流</param>
        /// <returns></returns>
        private static ISheet GetExcelSheet(string ext, Stream fs)
        {
            IWorkbook workbook = null;
            using (fs)
            {
                if (ext == ".xls")
                {
                    workbook = new HSSFWorkbook(fs);
                }
                else if (ext == ".xlsx")
                {
                    workbook = new XSSFWorkbook(fs);
                }
                return workbook.GetSheetAt(0);
            }
        }

        /// <summary>
        /// 获取Excel的行
        /// </summary>
        /// <param name="ext"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        private static IRow GetExcelRow(string ext, IEnumerator rows)
        {
            IRow row;
            if (ext == ".xls")
            {
                row = (HSSFRow)rows.Current;
            }
            else
            {
                row = (XSSFRow)rows.Current;
            }
            return row;
        }

        /// <summary>
        /// 将每行的数据读入数据表
        /// </summary>
        /// <param name="row"></param>
        /// <param name="dr"></param>
        private static void ReadDataFromExcelRow(IRow row, DataRow dr)
        {
            foreach (ICell item in row.Cells)
            {
                switch (item.CellType)
                {
                    case CellType.Boolean:
                        dr[item.ColumnIndex] = item.BooleanCellValue;
                        break;
                    case CellType.Numeric:
                        if (DateUtil.IsCellDateFormatted(item))
                        {
                            dr[item.ColumnIndex] = item.DateCellValue.ToString("yyyy-MM-dd hh:MM:ss");
                        }
                        else
                        {
                            dr[item.ColumnIndex] = item.NumericCellValue;
                        }
                        break;
                    case CellType.String:
                        if (!string.IsNullOrEmpty(item.StringCellValue))
                        {
                            dr[item.ColumnIndex] = item.StringCellValue.ToString();
                        }
                        else
                        {
                            dr[item.ColumnIndex] = null;
                        }
                        break;
                    case CellType.Unknown:
                    case CellType.Blank:
                    default:
                        dr[item.ColumnIndex] = string.Empty;
                        break;
                }
            }
        }
    }
}
