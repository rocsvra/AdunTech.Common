using System.ComponentModel;
using System.Reflection;

namespace AdunTech.IdcardOcr
{
    /// <summary>
    /// 枚举扩展类
    /// </summary>
    public static class Ext4Enum
    {
        /// <summary>
        /// 获取枚举描述
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">枚举值</param>
        /// <returns></returns>
        public static string GetDescription<T>(this T value) where T : struct
        {
            if (!typeof(T).IsEnum)
            {
                return null;
            }
            FieldInfo fieldInfo = typeof(T).GetField(value.ToString());
            var attr = (DescriptionAttribute)fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute), false);
            return attr?.Description;
        }

        /// <summary>
        /// 获取枚举名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumName<T>(this T value) where T : struct
        {
            if (!typeof(T).IsEnum)
            {
                return null;
            }
            FieldInfo fieldInfo = typeof(T).GetField(value.ToString());
            return fieldInfo.Name;
        }
    }
}
