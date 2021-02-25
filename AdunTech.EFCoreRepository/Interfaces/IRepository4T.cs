using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AdunTech.EFCoreRepository
{
    /// <summary>
    /// 仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// 存在实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Exists(object id);

        /// <summary>
        /// 获取实体(主键)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Find(object id);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> All();

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 查询（排序）
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderByExpression">排序表达式</param>
        /// <param name="isAsc">正序</param>
        /// <returns></returns>
        IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true);

        /// <summary>
        /// 获取分页记录
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex">从0开始</param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Page(int pageSize, int pageIndex, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 获取分页记录
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="predicate"></param>
        /// <param name="orderByExpression"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Page(
            int pageSize
            , int pageIndex
            , Expression<Func<TEntity, bool>> predicate
            , Expression<Func<TEntity, object>> orderByExpression
            , bool isAsc = true);

        /// <summary>
        /// 获取分页记录
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="predicate"></param>
        /// <param name="sortField">指定排序字段</param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Page(
            int pageSize
            , int pageIndex
            , Expression<Func<TEntity, bool>> predicate
            , string sortField
            , bool isAsc = true);
    }
}
