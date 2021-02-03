using System;
using System.Security.Cryptography;
using System.Text;

namespace AdunTech.Cryptography
{
    /// <summary>
    /// 哈希加密
    /// Secure Hash Algorithm，安全哈希算法
    /// 不可逆
    /// </summary>
    public class SHA
    {
        public string Encrypt(string text)
        {
            var bytes = Encoding.Default.GetBytes(text);
            var SHA = new SHA1CryptoServiceProvider();
            var encryptbytes = SHA.ComputeHash(bytes);
            return Convert.ToBase64String(encryptbytes);
        }

        public string Encrypt256(string text)
        {
            var bytes = Encoding.Default.GetBytes(text);
            var SHA256 = new SHA256CryptoServiceProvider();
            var encryptbytes = SHA256.ComputeHash(bytes);
            return Convert.ToBase64String(encryptbytes);
        }

        public string Encrypt384(string text)
        {
            var bytes = Encoding.Default.GetBytes(text);
            var SHA384 = new SHA384CryptoServiceProvider();
            var encryptbytes = SHA384.ComputeHash(bytes);
            return Convert.ToBase64String(encryptbytes);
        }

        public string Encrypt512(string text)
        {
            var bytes = Encoding.Default.GetBytes(text);
            var SHA512 = new SHA512CryptoServiceProvider();
            var encryptbytes = SHA512.ComputeHash(bytes);
            return Convert.ToBase64String(encryptbytes);
        }
    }
}
