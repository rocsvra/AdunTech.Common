using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Data;
using System.IO;

namespace AdunTech.Excel
{
    class ExportHelper
    {
        /// <summary>
        /// 导出
        /// </summary>
        public static MemoryStream GetExportFileStream(ExportOptions options)
        {
            if (options.DataScource == null || options.DataScource.Rows.Count == 0)
            {
                return null;
            }

            HSSFWorkbook hWorkBook = GetWorkSheet();
            ISheet sheet = hWorkBook.CreateSheet(options.SheetName);

            #region 输出表头
            int startRowIndex = 0;
            IRow headerRow;
            ICell headerCell;
            headerRow = sheet.CreateRow(startRowIndex);
            headerCell = headerRow.CreateCell(0);
            headerCell.SetCellValue("");
            startRowIndex++;

            //写入表名信息
            if (!string.IsNullOrEmpty(options.ReportName))
            {
                headerRow = sheet.CreateRow(startRowIndex);
                headerCell = headerRow.CreateCell(0);
                headerCell.SetCellValue(options.ReportName);
                ICellStyle style = hWorkBook.CreateCellStyle();
                style.Alignment = HorizontalAlignment.Center;
                IFont font = hWorkBook.CreateFont();
                font.FontHeight = 20 * 20;
                style.SetFont(font);
                headerCell.CellStyle = style;
                sheet.AddMergedRegion(new CellRangeAddress(startRowIndex, startRowIndex, 0, options.DataHeaders.Length - 1));
                startRowIndex++;
            }

            //IRow headerTitle = sheet.CreateRow(startRowIndex);
            //headerTitle.CreateCell(0).SetCellValue("填报人：");
            //headerTitle.CreateCell(1).SetCellValue("联系电话：");
            //startRowIndex++;
            headerRow = sheet.CreateRow(startRowIndex);
            headerCell = headerRow.CreateCell(0);

            int startHeaderCellIndex = 0;
            headerRow = sheet.CreateRow(startRowIndex);
            foreach (string header in options.DataHeaders)
            {
                headerCell = headerRow.CreateCell(startHeaderCellIndex);
                headerCell.SetCellValue(header);
                if (header.Length * 200 * 3 > 6000)
                {
                    sheet.SetColumnWidth(startHeaderCellIndex, header.Length * 200 * 3);
                }
                else
                {
                    sheet.SetColumnWidth(startHeaderCellIndex, 6000);
                }

                headerCell.CellStyle = SetHeaderStyle(hWorkBook);
                startHeaderCellIndex++;
            }
            startRowIndex++;
            #endregion

            #region 填充内容
            ICellStyle dateStyle = hWorkBook.CreateCellStyle();
            IDataFormat format = hWorkBook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");
            int rowIndex = startRowIndex;
            int serialNumber = 1;
            ICellStyle cellStyle = GetCellStyle(hWorkBook, 10, false);

            foreach (DataRow row in options.DataScource.Rows)
            {
                IRow dataRow = sheet.CreateRow(rowIndex);
                rowIndex++;
                for (int i = 0; i < options.DataColumnNames.Length; i++)
                {
                    ICell newCell = dataRow.CreateCell(i);
                    newCell.CellStyle = cellStyle;
                    string columnName = options.DataColumnNames[i];
                    //增加序号列
                    if (columnName == "{Index}")
                    {
                        newCell.SetCellValue(serialNumber.ToString());
                        serialNumber++;
                        continue;
                    }
                    DataColumn column = options.DataScource.Columns[columnName];
                    string drValue = row[column].ToString().Trim('/');
                    switch (column.DataType.ToString())
                    {
                        case "System.String":   //字符串类型
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime": //日期类型
                            if (drValue != "")
                            {
                                DateTime dateV;
                                DateTime.TryParse(drValue, out dateV);
                                newCell.SetCellValue(dateV);
                            }
                            else
                            {
                                newCell.SetCellValue(drValue);
                            }
                            break;
                        case "System.Boolean"://布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16"://整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal"://浮点型
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull"://空值处理
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }
                }
            }
            #endregion

            MemoryStream ms = new MemoryStream();
            hWorkBook.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }

        /// <summary>
        /// 工作表
        /// </summary>
        /// <param name="company"></param>
        /// <param name="author"></param>
        /// <param name="applicationName"></param>
        /// <param name="comments"></param>
        /// <param name="title"></param>
        /// <param name="subject"></param>
        /// <returns></returns>
        private static HSSFWorkbook GetWorkSheet(string company = "Adun", string author = "", string applicationName = "", string comments = "", string title = "", string subject = "")
        {
            HSSFWorkbook hssfworkbook = new HSSFWorkbook();
            //创建文件的摘要信息
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = company;
            hssfworkbook.DocumentSummaryInformation = dsi;
            //设置Excel文件的摘要信息
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Author = author;                         //填加xls文件作者信息
            si.ApplicationName = applicationName;       //填加xls文件创建程序信息
            si.Comments = comments;                     //填加xls文件作者信息
            si.Title = title;                           //填加xls文件标题信息
            si.Subject = subject;                       //填加文件主题信息
            si.CreateDateTime = DateTime.Now;
            hssfworkbook.SummaryInformation = si;
            return hssfworkbook;
        }

        /// <summary>
        /// 设置表头的样式
        /// </summary>
        /// <param name="workbook"></param>
        /// <returns></returns>
        private static ICellStyle SetHeaderStyle(HSSFWorkbook workbook)
        {
            //表头样式
            ICellStyle style = workbook.CreateCellStyle();
            //居中对齐       
            style.Alignment = HorizontalAlignment.Left;
            //表头单元格背景色
            style.FillForegroundColor = HSSFColor.Grey25Percent.Index;
            style.FillPattern = FillPattern.SolidForeground;
            //表头单元格边框
            style.BorderTop = BorderStyle.Thin;
            style.TopBorderColor = HSSFColor.Black.Index;
            style.BorderRight = BorderStyle.Thin;
            style.RightBorderColor = HSSFColor.Black.Index;
            style.BorderBottom = BorderStyle.Thin;
            style.BottomBorderColor = HSSFColor.Black.Index;
            style.BorderLeft = BorderStyle.Thin;
            style.LeftBorderColor = HSSFColor.Black.Index;
            //表头字体设置
            IFont font = workbook.CreateFont();
            font.FontHeightInPoints = 12;       //字号
            font.IsBold = true;                 //加粗
            style.SetFont(font);
            return style;
        }

        /// <summary>
        /// 设置单元格的样式
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="fontSize">字体大小</param>
        /// <param name="isBold">是否粗体</param>
        /// <returns></returns>
        private static ICellStyle GetCellStyle(HSSFWorkbook workbook, int fontSize, bool isBold)
        {
            ICellStyle style = workbook.CreateCellStyle();
            style.Alignment = HorizontalAlignment.Left;
            style.BorderTop = BorderStyle.Thin;
            style.TopBorderColor = HSSFColor.Black.Index;
            style.BorderBottom = BorderStyle.Thin;
            style.BottomBorderColor = HSSFColor.Black.Index;
            style.BorderLeft = BorderStyle.Thin;
            style.LeftBorderColor = HSSFColor.Black.Index;
            style.BorderRight = BorderStyle.Thin;
            style.RightBorderColor = HSSFColor.Black.Index;

            IFont font = workbook.CreateFont();
            font.FontHeightInPoints = (short)fontSize;
            font.IsBold = isBold;

            style.SetFont(font);
            return style;
        }
    }
}
