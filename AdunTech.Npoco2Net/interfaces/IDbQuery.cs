using NPoco;
using System.Collections.Generic;

namespace AdunTech.Npoco2Net
{
    /// <summary>
    /// 数据库接口（查询）
    /// </summary>
    public interface IDbQuery
    {
        /// <summary>
        /// 实体是否存在
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Exist<T>(object id);

        /// <summary>
        /// 查询单条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        T SingleOrDefault<T>(string sql, params object[] args);

        /// <summary>
        /// 查询单条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">主键</param>
        /// <returns></returns>
        T SingleOrDefaultById<T>(object id);

        /// <summary>
        /// 查询首条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        T First<T>(string sql, params object[] args);

        /// <summary>
        /// 查询首条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        T FirstOrDefault<T>(string sql, params object[] args);

        /// <summary>
        /// 数据查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        List<T> Fetch<T>();

        /// <summary>
        /// 数据查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        IEnumerable<T> Query<T>(string sql, params object[] args);

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageIndex">从1开始</param>
        /// <param name="pageSize"></param>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        Page<T> Page<T>(int pageIndex, int pageSize, string sql, params object[] args);
    }
}
