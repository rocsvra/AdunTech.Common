using AdunTech.Npoco2Net;
using Microsoft.Data.SqlClient;
using NPoco.DatabaseTypes;

namespace AdunTech.NPoco2SqlServer
{
    /// <summary>
    /// sqlserver 操作类
    /// </summary>
    public class SqlServerDb : NPocoDb, ISqlServerDb
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString"></param>
        public SqlServerDb(string connectionString)
            : base(connectionString, new SqlServerDatabaseType(), SqlClientFactory.Instance) { }
    }
}