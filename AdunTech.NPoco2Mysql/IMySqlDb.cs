using AdunTech.Npoco2Net;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AdunTech.NPoco2Mysql
{
    /// <summary>
    /// MySql数据库操作
    /// </summary>
    public interface IMySqlDb : INPocoDb
    {
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        DataTable ExecProcedure(string procName, MySqlParameter[] parameters = null);
    }
}
