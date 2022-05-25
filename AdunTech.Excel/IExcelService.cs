using System.Data;
using System.IO;

namespace AdunTech.Excel
{
    public interface IExcelService
    {
        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        MemoryStream Export(ExportOptions options);

        /// <summary>
        /// 导入
        /// </summary>
        /// <returns></returns>
        DataTable Import(ImportOptions options);
    }
}
