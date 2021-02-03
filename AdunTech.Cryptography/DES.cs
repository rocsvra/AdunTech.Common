using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AdunTech.Cryptography
{
    /// <summary>
    /// Des加密密钥（包含密钥和辅助向量）
    /// </summary>
    public class DesKey
    {
        /// <summary>
        /// 密钥
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 辅助向量
        /// </summary>
        public string IV { get; set; }
    }

    /// <summary>
    /// DES
    /// Data Encryption Algorithm，对称加密算法
    /// </summary>
    public class DES
    {
        /// <summary>
        /// 生产密钥
        /// </summary>
        /// <returns></returns>
        public static DesKey GenerateKey()
        {
            return new DesKey
            {
                Key = Generator.GenerateKey(64),
                IV = Generator.GenerateIV(64)
            };
        }

        /// <summary>  
        /// 加密
        /// </summary>  
        /// <param name="text">待加密串</param>  
        /// <param name="key">密钥（8位 64bit）</param>  
        /// <param name="iv">向量（8位 64bit）</param>  
        /// <returns></returns>  
        public static string Encrypt(string text, string key, string iv)
        {
            using (DESCryptoServiceProvider provider = new DESCryptoServiceProvider { Key = Encoding.UTF8.GetBytes(key), IV = Encoding.UTF8.GetBytes(iv) })
            {
                using (ICryptoTransform ct = provider.CreateEncryptor())
                {
                    byte[] b = Encoding.UTF8.GetBytes(text);
                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, ct, CryptoStreamMode.Write))
                        {
                            cs.Write(b, 0, b.Length);
                            cs.FlushFinalBlock();
                        }
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }

        /// <summary>  
        /// 解密  
        /// </summary>  
        /// <param name="text">待解密串</param>  
        /// <param name="key">密钥</param>  
        /// <param name="iv">向量</param>  
        public static string Decrypt(string text, string key, string iv)
        {
            using (DESCryptoServiceProvider provider = new DESCryptoServiceProvider { Key = Encoding.UTF8.GetBytes(key), IV = Encoding.UTF8.GetBytes(iv) })
            {
                using (ICryptoTransform ct = provider.CreateDecryptor())
                {
                    byte[] b = Convert.FromBase64String(text);
                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, ct, CryptoStreamMode.Write))
                        {
                            cs.Write(b, 0, b.Length);
                            cs.FlushFinalBlock();
                        }
                        return Encoding.UTF8.GetString(ms.ToArray());
                    }
                }
            }
        }
    }
}
