using AdunTech.CommonDomain;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdunTech.CommonInfra
{
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet;

        public EfRepository(IEfDbContext dbContext)
        {
            _dbSet = dbContext.Set<TEntity>();
        }

        public virtual TEntity Find([NotNull] object id)
        {
            var keyValues = new object[] { id };
            return _dbSet.Find(keyValues);
        }

        public int Count(ISpecification<TEntity> spec)
        {
            var specificationResult = ApplySpecification(spec);
            return specificationResult.Count();
        }

        public List<TEntity> All()
        {
            return _dbSet.ToList();
        }

        public List<TEntity> Query(ISpecification<TEntity> spec)
        {
            var specificationResult = ApplySpecification(spec);
            return specificationResult.ToList();
        }

        public List<TEntity> Query(ISpecification<TEntity> spec, int pageIndex, int pageSize, string sortField, bool isAsc, out long total)
        {
            var specificationResult = ApplySpecification(spec).OrderByField(sortField, isAsc);
            total = specificationResult.Count();
            return specificationResult.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }

        public TEntity First(ISpecification<TEntity> spec)
        {
            var specificationResult = ApplySpecification(spec);
            return specificationResult.First();
        }

        public virtual TEntity FirstOrDefault(ISpecification<TEntity> spec)
        {
            var specificationResult = ApplySpecification(spec);
            return specificationResult.FirstOrDefault();
        }


        #region 异步 
        public virtual async Task<TEntity> FindAsync([NotNull] object id, CancellationToken cancellationToken = default)
        {
            var keyValues = new object[] { id };
            return await _dbSet.FindAsync(keyValues, cancellationToken);
        }

        public async Task<int> CountAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default)
        {
            var specificationResult = ApplySpecification(spec);
            return await specificationResult.CountAsync(cancellationToken);
        }

        public async Task<List<TEntity>> AllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.ToListAsync(cancellationToken);
        }

        public async Task<List<TEntity>> QueryAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default)
        {
            var specificationResult = ApplySpecification(spec);
            return await specificationResult.ToListAsync(cancellationToken);
        }

        public async Task<List<TEntity>> QueryAsync(ISpecification<TEntity> spec, int pageIndex, int pageSize, string sortField, bool isAsc, CancellationToken cancellationToken = default)
        {
            var specificationResult = ApplySpecification(spec).OrderByField(sortField, isAsc);
            return await specificationResult.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        }

        public async Task<TEntity> FirstAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default)
        {
            var specificationResult = ApplySpecification(spec);
            return await specificationResult.FirstAsync(cancellationToken);
        }

        public async Task<TEntity> FirstOrDefaultAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default)
        {
            var specificationResult = ApplySpecification(spec);
            return await specificationResult.FirstOrDefaultAsync(cancellationToken);
        }

        #endregion

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
        {
            var evaluator = new SpecificationEvaluator();
            return evaluator.GetQuery(_dbSet.AsQueryable(), spec);
        }
    }
}
