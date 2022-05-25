using System.Collections.Generic;
using System.Data;

namespace AdunTech.CommonDomain
{
    public interface ISqlOperator
    {
        int ExecuteNonQuery(string sql, params object[] parameters);
        IEnumerable<T> ExecuteQuery<T>(string sql, params object[] parameters)
            where T : class, new();
        DataTable ExecuteQuery(string sql, params object[] parameters);
    }
}
