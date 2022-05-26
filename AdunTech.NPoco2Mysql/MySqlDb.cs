using AdunTech.Npoco2Net;
using MySql.Data.MySqlClient;
using NPoco;
using System.Data;

namespace AdunTech.NPoco2Mysql
{
    /// <summary>
    /// mysql 操作类
    /// </summary>
    public class MySqlDb : NPocoDb, IMySqlDb
    {
        private readonly string _connectionString;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString"></param>
        public MySqlDb(string connectionString)
            : base(connectionString, DatabaseType.MySQL, MySqlClientFactory.Instance)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataTable ExecProcedure(string procName, MySqlParameter[] parameters = null)
        {
            DataTable tbl = null;
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(procName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null && parameters.Length > 0)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    connection.Open();
                    command.ExecuteNonQuery();
                    tbl = new DataTable();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    adapter.Fill(tbl);
                    connection.Close();
                }
            }
            return tbl;
        }
    }
}
