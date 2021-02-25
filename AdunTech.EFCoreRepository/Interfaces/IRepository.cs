using System.Collections.Generic;
using System.Data;

namespace AdunTech.EFCoreRepository
{
    /// <summary>
    /// 仓储
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// 根据sql 返回泛型类集列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        IEnumerable<T> Query<T>(string sql) where T : class, new();

        /// <summary>
        /// 根据sql 返回DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        DataTable Query(string sql);
    }
}
