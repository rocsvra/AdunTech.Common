using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace AdunTech.CommonInfra
{
    public interface IEfDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        EntityEntry<TEntity> Entry<TEntity>([NotNull] TEntity entity) where TEntity : class;
        EntityEntry<TEntity> Add<TEntity>([NotNull] TEntity entity) where TEntity : class;
        void AddRange([NotNull] IEnumerable<object> entities);
        EntityEntry<TEntity> Attach<TEntity>([NotNull] TEntity entity) where TEntity : class;
        void AttachRange([NotNull] IEnumerable<object> entities);
        EntityEntry<TEntity> Update<TEntity>([NotNull] TEntity entity) where TEntity : class;
        void UpdateRange([NotNull] IEnumerable<object> entities);
        IQueryable<TResult> FromExpression<TResult>([NotNull] Expression<Func<IQueryable<TResult>>> expression);
        EntityEntry<TEntity> Remove<TEntity>([NotNull] TEntity entity) where TEntity : class;
        void RemoveRange([NotNull] IEnumerable<object> entities);
        ChangeTracker ChangeTracker { get; }
        DatabaseFacade Database { get; }
        int SaveChanges(bool acceptAllChangesOnSuccess);
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
        void Dispose();
        ValueTask DisposeAsync();
    }
}
