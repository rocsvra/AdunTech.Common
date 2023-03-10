using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace AdunTech.CommonDomain
{
    /// <summary>
    /// 维护对象状态，统一提交更改
    /// </summary>
    public interface IUnitOfWork
    {
        void Add<TEntity>(TEntity entity)
            where TEntity : class, new();
        void AddRange<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class, new();
        void Modify<TEntity>(TEntity entity)
            where TEntity : class, new();
        void ModifyRange<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class, new();
        void Modify4Aggregate<TEntity>(TEntity entity)
            where TEntity : class, new();
        void ModifyRange4Aggregate<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class, new();
        void Remove<TEntity>(TEntity entity)
            where TEntity : class, new();
        void RemoveRange<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class, new();
        bool Commit();
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
        void Rollback();
    }
}
