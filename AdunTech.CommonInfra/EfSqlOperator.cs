using AdunTech.CommonDomain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;


namespace AdunTech.CommonInfra
{
    public class EfSqlOperator : ISqlOperator
    {
        private readonly IEfDbContext _dbContext;

        public EfSqlOperator(IEfDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int ExecuteNonQuery(string sql, params object[] parameters)
        {
            return _dbContext.Database.ExecuteSqlRaw(sql, parameters);
        }

        public IEnumerable<T> ExecuteQuery<T>(string sql, params object[] parameters)
            where T : class, new()
        {
            return _dbContext.Database.SqlQuery<T>(sql, parameters);
        }

        public DataTable ExecuteQuery(string sql, params object[] parameters)
        {
            return _dbContext.Database.SqlQuery(sql, parameters);
        }
    }
}
