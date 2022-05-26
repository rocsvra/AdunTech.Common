using System.Collections.Generic;

namespace AdunTech.Npoco2Net
{
    /// <summary>
    /// 数据库接口（非查询）
    /// </summary>
    public interface IDbNonQuery
    {
        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        int ExecuteNonQuery(string sql, params object[] args);

        /// <summary>
        /// 执行sql(批量)
        /// </summary>
        /// <param name="sqls"></param>
        void ExecuteNonQuery(IEnumerable<string> sqls);

        /// <summary>
        /// 插入实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        object Insert<T>(T entity);

        /// <summary>
        /// 插入(批量)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        int InsertBatch<T>(IEnumerable<T> entities);

        /// <summary>
        /// 插入(巨量)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        void InsertBulk<T>(IEnumerable<T> entities);

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Update(object entity);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Delete(object entity);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">主键</param>
        /// <returns></returns>
        int Delete<T>(object id);
    }
}
