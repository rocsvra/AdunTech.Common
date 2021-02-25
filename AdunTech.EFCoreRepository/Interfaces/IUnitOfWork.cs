using System.Collections.Generic;

namespace AdunTech.EFCoreRepository
{
    /// <summary>
    /// 维护对象状态，统一提交更改
    /// </summary>
    public interface IUnitOfWork
    {
        void Add<TEntity>(TEntity entity) where TEntity : class;

        TEntity AddAndCommit<TEntity>(TEntity entity) where TEntity : class;

        void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        void Remove<TEntity>(TEntity entity) where TEntity : class;

        void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        void Modify<TEntity>(TEntity entity) where TEntity : class;

        void ModifyAndCommit<TEntity>(TEntity entity) where TEntity : class;

        void ModifyRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        int ExecuteSqlCommand(string sql);

        bool Commit();

        void Rollback();
    }
}
