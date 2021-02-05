using System;

namespace AdunTech.Co2Net.Helpers
{
    /// <summary>
    /// 时间戳
    /// </summary>
    public class TimeStampHelper
    {
        /// <summary>
        /// Unix起始时间
        /// </summary>
        private readonly static DateTimeOffset BaseTime = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);

        /// <summary>  
        /// 获取Unix时间戳（秒）
        /// Unix时间戳：从1970年1月1日（UTC/GMT的午夜）开始所经过的秒数，不考虑闰秒
        /// </summary>  
        /// <returns>long </returns>  
        public static long GetTimeStamp()
        {
            return (long)(DateTime.UtcNow - BaseTime).TotalSeconds;
        }

        /// <summary>  
        /// 获取Unix时间戳（秒）
        /// Unix时间戳：从1970年1月1日（UTC/GMT的午夜）开始所经过的秒数，不考虑闰秒
        /// </summary>  
        /// <param name="dateTime"></param>  
        /// <returns>long </returns>  
        public static long GetTimeStamp(DateTime dateTime)
        {
            return (long)(dateTime.ToUniversalTime() - BaseTime).TotalSeconds;
        }

        /// <summary>        
        /// 时间戳转DateTime  
        /// </summary>        
        /// <param name="timeStamp">Unix时间戳（秒）</param>        
        /// <returns></returns>        
        public static DateTime GetDateTime(long timeStamp)
        {
            return BaseTime.AddSeconds(timeStamp).LocalDateTime;
        }
    }
}
