using Ardalis.Specification;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
        TEntity Find([NotNull] object id);
        int Count(ISpecification<TEntity> spec);
        List<TEntity> All();
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
        TEntity First(ISpecification<TEntity> spec);
        TEntity FirstOrDefault(ISpecification<TEntity> spec);

        Task<TEntity> FindAsync([NotNull] object id, CancellationToken cancellationToken = default);
        Task<int> CountAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default);
        Task<List<TEntity>> AllAsync(CancellationToken cancellationToken = default);
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
        Task<TEntity> FirstAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default);
        Task<TEntity> FirstOrDefaultAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default);
    }
}
