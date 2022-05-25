using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AdunTech.Excel
{
    /// <summary>
    /// 导出参数
    /// </summary>
    public class ExportOptions
    {
        /// <summary>
        /// 导出报表显示列头
        /// </summary>
        public string[] DataHeaders
        {
            get
            {
                if (DataColumnMap == null || DataColumnMap.Count == 0)
                {
                    var data = new string[DataScource.Columns.Count];
                    for (int i = 0; i < DataScource.Columns.Count; i++)
                    {
                        data[i] = DataScource.Columns[i].ColumnName;
                    }
                    return data;
                }
                return DataColumnMap.Select(o => o.Value).ToArray();
            }
        }

        /// <summary>
        /// 数据源对应列名
        /// </summary>
        public string[] DataColumnNames
        {
            get
            {
                if (DataColumnMap == null || DataColumnMap.Count == 0)
                {
                    var data = new string[DataScource.Columns.Count];
                    for (int i = 0; i < DataScource.Columns.Count; i++)
                    {
                        data[i] = DataScource.Columns[i].ColumnName;
                    }
                    return data;
                }
                return DataColumnMap.Select(o => o.Key).ToArray();
            }
        }

        /// <summary>
        /// 数据源（必填）
        /// </summary>
        public DataTable DataScource { get; set; }

        /// <summary>
        /// 需显示列名以及对应列头名称
        /// 格式：key:数据列名;value:显示列头
        /// 如果该属性为空，默认显示DateTable中的所有列，列名即列头
        /// </summary>
        public Dictionary<string, string> DataColumnMap { get; set; }

        /// <summary>
        /// Execl文件中的Sheet名称（选填）
        /// </summary>
        public string SheetName { get; set; } = DateTime.Now.ToString("yyyyMMdd");

        /// <summary>
        /// 表名信息（选填）
        /// </summary>
        public string ReportName { get; set; }
    }
}
