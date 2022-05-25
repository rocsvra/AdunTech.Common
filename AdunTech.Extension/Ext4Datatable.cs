using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace AdunTech.Extension
{
    /// <summary>
    ///  List/IEnumerable和DataTable互相转化
    /// </summary>
    public static class Ext4Datatable
    {
        /// <summary>
        /// DataTable 转 List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static IList<T> ToIList<T>(this DataTable table)
        {
            if (table == null)
            {
                return null;
            }

            List<DataRow> rows = new List<DataRow>();
            foreach (DataRow row in table.Rows)
            {
                rows.Add(row);
            }
            return ConvertTo<T>(rows);
        }

        /// <summary>
        /// List 转 DataTable
        /// </summary>
        public static DataTable ToDataTable<T>(this List<T> items) where T : class
        {
            var tbl = new DataTable(typeof(T).Name);
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in props)
            {
                Type t = GetCoreType(prop.PropertyType);
                tbl.Columns.Add(prop.Name, t);
            }

            foreach (T item in items)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }
                tbl.Rows.Add(values);
            }
            return tbl;
        }

        /// <summary>
        /// IEnumerable 转 DataTable
        /// </summary>
        public static DataTable ToDataTable<T>(this IEnumerable<T> collection) where T : class
        {
            var props = typeof(T).GetProperties();
            var dt = new DataTable();
            dt.Columns.AddRange(props.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray());
            if (collection.Count() > 0)
            {
                for (int i = 0; i < collection.Count(); i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in props)
                    {
                        object obj = pi.GetValue(collection.ElementAt(i), null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    dt.LoadDataRow(array, true);
                }
            }
            return dt;
        }

        private static Type GetCoreType(Type t)
        {
            bool isNullable = !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
            if (t != null && isNullable)
            {
                return !t.IsValueType ? t : Nullable.GetUnderlyingType(t);
            }
            return t;
        }

        private static IList<T> ConvertTo<T>(IList<DataRow> rows)
        {
            IList<T> list = null;
            if (rows != null)
            {
                list = new List<T>();
                foreach (DataRow row in rows)
                {
                    T item = CreateItem<T>(row);
                    list.Add(item);
                }
            }
            return list;
        }

        private static T CreateItem<T>(DataRow row)
        {
            T obj = default(T);
            if (row != null)
            {
                obj = Activator.CreateInstance<T>();

                foreach (DataColumn column in row.Table.Columns)
                {
                    PropertyInfo prop = obj.GetType().GetProperty(column.ColumnName);
                    object value = row[column.ColumnName];
                    prop.SetValue(obj, value, null);
                }
            }
            return obj;
        }
    }
}
