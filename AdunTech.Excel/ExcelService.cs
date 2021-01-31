using System.Data;
using System.IO;

namespace AdunTech.Excel
{
    public class ExcelService : IExcelService
    {
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public MemoryStream Export(ExportOptions options)
        {
            return ExportHelper.GetExportFileStream(options);
        }

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public DataTable Import(ImportOptions options)
        {
            return ImportHelper.Read(options);
        }
    }
}
