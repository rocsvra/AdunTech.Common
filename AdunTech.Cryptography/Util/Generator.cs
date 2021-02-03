using System;
using System.Text;

namespace AdunTech.Cryptography
{
    /// <summary>
    /// 密钥生成器
    /// </summary>
    public class Generator
    {
        private readonly static char[] chars = new char[]
        {
            'a','b','d','c','e','f','g','h','i','j','k','l','m','n','p','r','q','s','t','u','v','w','z','y','x',
            '0','1','2','3','4','5','6','7','8','9',
            'A','B','C','D','E','F','G','H','I','J','K','L','M','N','Q','P','R','T','S','V','U','W','X','Y','Z'
        };

        /// <summary>
        /// 随机加密密钥
        /// </summary>
        /// <param name="keySize">位数（256、128）</param>
        /// <returns></returns>
        public static string GenerateKey(int keySize)
        {
            StringBuilder keyStr = new StringBuilder();
            Random rnd = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < keySize / 8; i++)
            {
                keyStr.Append(chars[rnd.Next(0, chars.Length)].ToString());
            }
            return keyStr.ToString();
        }

        /// <summary>
        /// 随机初始化向量
        /// </summary>
        /// <returns></returns>
        public static string GenerateIV(int keySize)
        {
            StringBuilder num = new StringBuilder();
            Random rnd = new Random(DateTime.Now.AddHours(1).Millisecond);
            for (int i = 0; i < keySize / 8; i++)
            {
                num.Append(chars[rnd.Next(0, chars.Length)].ToString());
            }
            return num.ToString();
        }
    }
}
