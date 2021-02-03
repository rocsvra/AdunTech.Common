using System;
using System.Text;

namespace AdunTech.Cryptography
{
    /// <summary>
    /// MD5
    /// Message-Digest Algorithm，不可逆加密，数字指纹
    /// </summary>
    public class MD5
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="data">待加密串</param>
        public static string Encrypt(string data)
        {
            return Encrypt(data, Encoding.UTF8);
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="data">待加密串</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string Encrypt(string data, Encoding encoding)
        {
            var md5 = System.Security.Cryptography.MD5.Create();
            var dataByte = md5.ComputeHash(encoding.GetBytes(data));
            return BitConverter.ToString(dataByte).Replace("-", "");
        }
    }
}
