using System.IO;
using System.Linq;

namespace AdunTech.Excel
{
    /// <summary>
    /// 导入配置参数
    /// </summary>
    public class ImportOptions
    {
        private readonly string[] _allowExtentions = new string[] { ".xls", ".xlsx" };

        private string _ext;
        /// <summary>
        /// execl文件后缀
        /// </summary>
        public string Ext
        {
            get => _ext.ToLower();
            set => _ext = value;
        }

        /// <summary>
        /// 是否excel文档
        /// </summary>
        public bool IsExcleFile => _allowExtentions.Contains(Ext);

        /// <summary>
        /// 文件流
        /// </summary>
        public Stream File { get; set; }

        /// <summary>
        /// 列名所在行数,行索引从0开始
        /// </summary>
        public int ColumnNameRowStartIndex { get; set; } = 0;
    }
}
