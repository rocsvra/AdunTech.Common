using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AdunTech.SapRfc
{
    public static class Ext4SapRfc
    {
        public static IEnumerable<TOutput> Query<TOutput>(this IRfcFunction invokedRfcFunc)
        {
            var innerTableName = typeof(TOutput).Name;
            IRfcTable rfcTbl = invokedRfcFunc.GetTable(innerTableName);
            return rfcTbl.Query<TOutput>();
        }

        public static IList<T> Query<T>(this IRfcTable rfcTable)
        {
            if (rfcTable.RowCount == 0)
            {
                return null;
            }
            IList<T> list = new List<T>();
            foreach (IRfcStructure struc in rfcTable)
            {
                T obj = Activator.CreateInstance<T>();
                for (int i = 0; i <= rfcTable.ElementCount - 1; i++)
                {
                    RfcElementMetadata metadata = rfcTable.GetElementMetadata(i);
                    var key = metadata.Name;
                    PropertyInfo prop = obj.GetType().GetProperty(key);
                    if (prop != null)
                    {
                        object value = null;
                        switch (metadata.DataType)
                        {
                            case RfcDataType.INT1:
                            case RfcDataType.INT2:
                            case RfcDataType.INT4:
                                value = struc.GetInt(key);
                                break;
                            case RfcDataType.INT8:
                                value = struc.GetLong(key);
                                break;
                            case RfcDataType.FLOAT:
                                value = struc.GetDouble(key);
                                break;
                            case RfcDataType.BCD:
                                value = struc.GetDecimal(key);
                                break;
                            default:
                                value = struc.GetString(key);
                                break;
                        }
                        prop.SetValue(obj, value, null);
                    }
                }
                list.Add(obj);
            }
            return list;
        }
    }
}
