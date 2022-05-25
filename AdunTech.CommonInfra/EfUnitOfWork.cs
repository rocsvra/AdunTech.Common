using AdunTech.CommonDomain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace AdunTech.CommonInfra
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly IEfDbContext _dbContext;

        public EfUnitOfWork(IEfDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add<TEntity>([NotNull] TEntity entity)
            where TEntity : class, new()
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public void AddRange<TEntity>([NotNull] IEnumerable<TEntity> entities)
            where TEntity : class, new()
        {
            _dbContext.Set<TEntity>().AddRange(entities);
        }

        public void Modify<TEntity>([NotNull] TEntity entity)
            where TEntity : class, new()
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void ModifyRange<TEntity>([NotNull] IEnumerable<TEntity> entities)
            where TEntity : class, new()
        {
            foreach (var entity in entities) this.Modify(entity);
        }

        public void Modify4Aggregate<TEntity>([NotNull] TEntity entity)
            where TEntity : class, new()
        {
            _dbContext.Update(entity);
        }

        public void ModifyRange4Aggregate<TEntity>([NotNull] IEnumerable<TEntity> entities)
            where TEntity : class, new()
        {
            _dbContext.UpdateRange(entities);
        }

        public void Remove<TEntity>([NotNull] TEntity entity)
            where TEntity : class, new()
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange<TEntity>([NotNull] IEnumerable<TEntity> entities)
            where TEntity : class, new()
        {
            _dbContext.Set<TEntity>().RemoveRange(entities);
        }

        public bool Commit()
        {
            return _dbContext.SaveChanges() > 0;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Rollback()
        {
            var entries = _dbContext.ChangeTracker.Entries();
            if (entries != null)
            {
                foreach (var entry in entries) entry.State = EntityState.Unchanged;
            }
        }
    }
}
