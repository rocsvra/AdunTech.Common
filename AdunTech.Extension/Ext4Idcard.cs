using System;

namespace AdunTech.Extension
{
    /// <summary>
    /// 身份证扩展类
    /// </summary>
    public static class Ext4Idcard
    {
        /// <summary>
        /// 获取生日
        /// 说明：身份证号错误，返回null
        /// </summary>
        /// <param name="idcard"></param>
        /// <returns></returns>
        public static DateTime? GetBirthday(this string idcard)
        {
            if (idcard.Length == 15)
            {
                return DateTime.Parse("19" + idcard.Substring(6, 2) + "-" + idcard.Substring(8, 2) + "-" + idcard.Substring(10, 2));
            }
            else if (idcard.Length == 18)
            {
                return DateTime.Parse(idcard.Substring(6, 4) + "-" + idcard.Substring(10, 2) + "-" + idcard.Substring(12, 2));
            }
            return null;
        }

        /// <summary>
        /// 获取年龄
        /// </summary>
        /// <returns></returns>
        public static int GetAge(this string idcard)
        {
            DateTime? birthDay = GetBirthday(idcard);
            if (birthDay == null)
            {
                return -1;
            }
            int age = DateTime.Now.Year - ((DateTime)birthDay).Year;
            if (DateTime.Now.Month < ((DateTime)birthDay).Month
                || (DateTime.Now.Month == ((DateTime)birthDay).Month && DateTime.Now.Day < ((DateTime)birthDay).Day))
            {
                age--;
            }
            return age;
        }

        /// <summary>
        /// 获取性别 
        /// 男：1 女：0 未知：2
        /// </summary>
        /// <param name="idcard"></param>
        /// <returns></returns>
        public static int GetSex(this string idcard)
        {
            if (idcard.Length == 15)
            {
                return int.Parse(idcard.Substring(12, 3)) % 2;
            }
            else if (idcard.Length == 18)
            {
                return int.Parse(idcard.Substring(14, 3)) % 2;
            }
            return 2;
        }
    }
}
