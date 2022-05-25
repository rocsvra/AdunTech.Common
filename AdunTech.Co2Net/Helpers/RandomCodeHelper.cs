using System;

namespace AdunTech.Co2Net.Helpers
{
    /// <summary>
    /// 随机码生成类
    /// </summary>
    public static class RandomCodeHelper
    {
        /// <summary>
        /// 获取由字母与数字组成的、指定位数随机码
        /// </summary>
        /// <param name="length">随机码位数</param>
        /// <returns>返回随机码</returns>
        public static string GenerateCode(int length)
        {
            string allChar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,W,X,Y,Z";
            string[] allCharArray = allChar.Split(',');
            string randomCode = "";
            int temp = -1;

            Random rand = new Random();
            for (int i = 0; i < length; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(35);
                if (temp == t)
                {
                    return GenerateCode(length);
                }
                temp = t;
                randomCode += allCharArray[t];
            }
            return randomCode;
        }

        /// <summary>
        /// 获取由仅由数字组成的、指定位数随机码
        /// </summary>
        /// <param name="length">随机码位数</param>
        /// <returns>返回随机码</returns>
        public static string GenerateDigitCode(int length)
        {
            string allChar = "0,1,2,3,4,5,6,7,8,9";
            string[] allCharArray = allChar.Split(',');
            string randomCode = "";
            int temp = -1;

            Random rand = new Random();
            for (int i = 0; i < length; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(10);
                if (temp == t)
                {
                    return GenerateDigitCode(length);
                }
                temp = t;
                randomCode += allCharArray[t];
            }
            return randomCode;
        }

        /// <summary>
        /// 根据GUID获取16位的唯一字符串  
        /// </summary>
        /// <returns></returns>
        public static string GenerateStringID()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }

        /// <summary>
        /// 根据GUID获取19位的唯一数字序列
        /// </summary>
        /// <returns></returns>
        public static long GenerateLongID()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 0);
        }

        /// <summary>
        /// 有序(时间)Id
        /// </summary>
        /// <returns></returns>
        public static string GenerateSequentialId()
        {
            var time = DateTime.Now.ToUniversalTime().Ticks / 10000;
            var timeStr = time.ToString("x8").PadLeft(13, '0');
            return string.Format("{0}-{1}", timeStr, Guid.NewGuid());
        }
    }
}
