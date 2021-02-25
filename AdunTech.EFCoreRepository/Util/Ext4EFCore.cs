using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace AdunTech.EFCoreRepository
{
    public static class Ext4EFCore
    {
        public static DataTable SqlQuery(this DatabaseFacade db, string sql, params object[] parameters)
        {
            var dt = new DataTable();
            var conn = db.GetDbConnection();
            try
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);
                    using (var reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                        reader.Close();
                    }
                }
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public static IEnumerable<T> SqlQuery<T>(this DatabaseFacade db, string sql, params object[] parameters) where T : class, new()
        {
            var list = new List<T>();
            var propertyInfos = typeof(T).GetProperties();
            var dt = SqlQuery(db, sql, parameters);
            foreach (DataRow row in dt.Rows)
            {
                var t = new T();
                foreach (PropertyInfo p in propertyInfos)
                {
                    if (dt.Columns.IndexOf(p.Name) != -1 && row[p.Name] != DBNull.Value)
                    {
                        p.SetValue(t, row[p.Name], null);
                    }
                }
                list.Add(t);
            }
            return list;
        }
    }
}
