using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AdunTech.EFCoreRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbContext _dbContext;

        public UnitOfWork(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity != null)
            {
                _dbContext.Set<TEntity>().Add(entity);
            }
        }

        public TEntity AddAndCommit<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity != null)
            {
                _dbContext.Set<TEntity>().Add(entity);
                _dbContext.SaveChanges();
            }
            return entity;
        }

        public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            if (entities != null)
            {
                _dbContext.Set<TEntity>().AddRange(entities);
            }
        }

        public void Modify<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity != null)
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
        }

        public void ModifyAndCommit<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity != null)
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
        }

        public void ModifyRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            if (entities != null)
            {
                foreach (var entity in entities) this.Modify(entity);
            }
        }

        public void Remove<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity != null)
            {
                _dbContext.Set<TEntity>().Remove(entity);
            }
        }

        public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            if (entities != null)
            {
                _dbContext.Set<TEntity>().RemoveRange(entities);
            }
        }

        public int ExecuteSqlCommand(string sql)
        {
            return _dbContext.Database.ExecuteSqlRaw(sql);
        }

        public bool Commit()
        {
            return _dbContext.SaveChanges() > 0;
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