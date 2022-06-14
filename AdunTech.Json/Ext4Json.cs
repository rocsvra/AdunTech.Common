using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace AdunTech.Json
{
    /// <summary>
    /// JsonSerializer
    /// </summary>
    public static class Ext4Json
    {
        /// <summary>
        /// 序列化为json字符串
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string JsonSerialize(this object source)
        {
            return JsonConvert.SerializeObject(source);
        }

        /// <summary>
        /// 序列化为json字符串
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dateTimeFormats">exp:"yyyy-MM-dd HH:mm:ss" </param>
        /// <returns></returns>
        public static string JsonSerialize(this object source, string dateTimeFormats)
        {
            return JsonConvert.SerializeObject(source, new IsoDateTimeConverter { DateTimeFormat = dateTimeFormats });
        }

        /// <summary>
        /// 反序列化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T JsonDeserialize<T>(this string source) where T : class
        {
            return JsonConvert.DeserializeObject<T>(source);
        }

        /// <summary>
        /// 转化为JObject对象
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static JObject ToJObject(this string source)
        {
            return JObject.Parse(source);
        }
    }
}
