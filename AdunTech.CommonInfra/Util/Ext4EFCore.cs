using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace AdunTech.CommonInfra
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

        /// <summary>
        /// 按字段动态排序
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="sortField"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public static IQueryable<T> OrderByField<T>(this IQueryable<T> queryable, string sortField, bool isAsc = true)
            where T : class
        {
            if (string.IsNullOrEmpty(sortField))
            {
                throw new Exception("排序参数错误");
            }
            PropertyInfo p = typeof(T).GetProperty(sortField, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (p == null)
            {
                throw new Exception("排序参数错误，字段名有误");
            }
            ParameterExpression param = Expression.Parameter(typeof(T), sortField);
            Expression expr = Expression.Call(typeof(Queryable)
                                           , isAsc ? "OrderBy" : "OrderByDescending"
                                           , new Type[] { typeof(T), p.PropertyType }
                                           , queryable.Expression
                                           , Expression.Lambda(Expression.Property(param, sortField), param));
            return queryable.AsQueryable().Provider.CreateQuery<T>(expr);
        }
    }
}
