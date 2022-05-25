using System.Collections.Generic;

namespace AdunTech.Co2Net.Models
{
    /// <summary>
    /// 分页记录
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PaginatedItems<T> where T : class
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="total"></param>
        /// <param name="data"></param>
        public PaginatedItems(int pageIndex, int pageSize, int total, IEnumerable<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Data = data;
            Total = total;
        }

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; private set; }
        /// <summary>
        /// 每页记录数
        /// </summary>
        public int PageSize { get; private set; }
        /// <summary>
        /// 记录总数
        /// </summary>
        public int Total { get; private set; }
        /// <summary>
        /// 数据集合
        /// </summary>
        public IEnumerable<T> Data { get; private set; }
    }
}
