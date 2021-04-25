using AdunTech.CommonDomain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace AdunTech.CommonInfra
{
    public class BaseRepository : IRepository
    {
        private IDbContext _dbContext;

        public BaseRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<T> Query<T>(string sql) where T : class, new()
        {
            return _dbContext.Database.SqlQuery<T>(sql);
        }

        public DataTable Query(string sql)
        {
            return _dbContext.Database.SqlQuery(sql);
        }
    }

    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// 数据集
        /// </summary>
        protected readonly DbSet<TEntity> DataSet;

        public BaseRepository(IDbContext dbContext)
        {
            DataSet = dbContext.Set<TEntity>();
        }

        public virtual bool Exists(object id)
        {
            return DataSet.Find(id) != null;
        }

        public virtual TEntity Find(object id)
        {
            return id == null ? null : DataSet.Find(id);
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return DataSet.FirstOrDefault(predicate);
        }

        public virtual IEnumerable<TEntity> All()
        {
            return DataSet.AsEnumerable();
        }

        public virtual IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> predicate)
        {
            return DataSet.Where(predicate).AsEnumerable();
        }

        public IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true)
        {
            var data = DataSet.Where(predicate);
            data = isAsc
                ? data.OrderBy(orderByExpression)
                : data.OrderByDescending(orderByExpression);
            return data.AsEnumerable();
        }

        public virtual IEnumerable<TEntity> Page(int pageSize, int pageIndex, Expression<Func<TEntity, bool>> predicate)
        {
            return DataSet.Where(predicate).Skip(pageSize * pageIndex).Take(pageSize).AsEnumerable();
        }

        public virtual IEnumerable<TEntity> Page(
            int pageSize
            , int pageIndex
            , Expression<Func<TEntity, bool>> predicate
            , Expression<Func<TEntity, object>> orderByExpression
            , bool isAsc = true)
        {
            var data = DataSet.Where(predicate);
            data = isAsc
                ? data.OrderBy(orderByExpression)
                : data.OrderByDescending(orderByExpression);
            return data.Skip(pageSize * pageIndex).Take(pageSize).AsEnumerable();
        }

        public IEnumerable<TEntity> Page(
            int pageSize
            , int pageIndex
            , Expression<Func<TEntity, bool>> predicate
            , string sortField = ""
            , bool isAsc = true)
        {
            if (string.IsNullOrEmpty(sortField))
            {
                return Page(pageSize, pageIndex, predicate);
            }

            PropertyInfo p = typeof(TEntity).GetProperty(sortField, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (p == null)
            {
                throw new Exception("排序字段不存在");
            }
            var data = DataSet.Where(predicate);

            Type[] types = new Type[2];   //参数：对象类型，属性类型
            types[0] = typeof(TEntity);
            types[1] = p.PropertyType;
            ParameterExpression param = Expression.Parameter(typeof(TEntity), sortField);
            Expression expr = Expression.Call(typeof(Queryable)
                                            , isAsc ? "OrderBy" : "OrderByDescending"
                                            , types
                                            , data.Expression
                                            , Expression.Lambda(Expression.Property(param, sortField), param));

            data = data.AsQueryable().Provider.CreateQuery<TEntity>(expr);
            return data.Skip(pageSize * pageIndex).Take(pageSize).AsEnumerable();
        }
    }
}
