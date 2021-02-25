using System.Collections.Generic;
using System.Data;

namespace AdunTech.EFCoreRepository
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
}
