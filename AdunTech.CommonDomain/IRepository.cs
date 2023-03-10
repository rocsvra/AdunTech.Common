using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace AdunTech.CommonDomain
{
    /// <summary>
    /// 负责查询
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// 根据Id查找记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Find(object id);
        /// <summary>
        /// 查询记录数量
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 查询记录数量
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        int Count(ISpecification<TEntity> spec);
        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        List<TEntity> All();
        /// <summary>
        /// 查询记录
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        List<TEntity> Query(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 查询记录
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        List<TEntity> Query(ISpecification<TEntity> spec);
        /// <summary>
        /// 根据字段名称，动态查询
        /// </summary>
        /// <param name="spec">规约条件</param>
        /// <param name="pageIndex">从0开始</param>
        /// <param name="pageSize"></param>
        /// <param name="sortField">排序字段</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="total">记录总数</param>
        /// <returns></returns>
        List<TEntity> Query(ISpecification<TEntity> spec, int pageIndex, int pageSize, string sortField, bool isAsc, out long total);
        /// <summary>
        /// 第一条记录
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity First(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 第一条记录
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        TEntity First(ISpecification<TEntity> spec);
        /// <summary>
        /// 第一条记录（可空）
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 第一条记录（可空）
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        TEntity FirstOrDefault(ISpecification<TEntity> spec);

        Task<TEntity> FindAsync(object id, CancellationToken cancellationToken = default);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<int> CountAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default);
        Task<List<TEntity>> AllAsync(CancellationToken cancellationToken = default);
        Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<List<TEntity>> QueryAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default);
        /// <summary>
        /// 根据字段名称，动态查询
        /// </summary>
        /// <param name="spec">规约条件</param>
        /// <param name="pageIndex">从0开始</param>
        /// <param name="pageSize">排序字段</param>
        /// <param name="sortField"></param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<TEntity>> QueryAsync(ISpecification<TEntity> spec, int pageIndex, int pageSize, string sortField, bool isAsc, CancellationToken cancellationToken = default);
        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<TEntity> FirstAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<TEntity> FirstOrDefaultAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default);
    }
}
