using System;
using System.Security.Cryptography;
using System.Text;

namespace AdunTech.Cryptography
{
    /// <summary>
    /// RSA公钥
    /// </summary>
    public class RsaPublicKey
    {
        public string Modulus { get; set; }
        public string Exponent { get; set; }
    }

    /// <summary>
    /// RSA密钥（包括公钥和私钥）
    /// </summary>
    public class RsaKey
    {
        public RsaPublicKey PublicKey { get; set; }
        public string PrivateKey { get; set; }
    }

    /// <summary>
    /// RSA（1024位）
    /// 一种非对称加密，公钥加密，私钥解密
    /// </summary>
    public class RSA
    {
        public static int KEYSIZE = 1024;
        /// <summary>
        /// 生成公私钥
        /// </summary>
        public static RsaKey GenerateKey()
        {
            RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider(KEYSIZE);
            RSAParameters publicParam = rsaProvider.ExportParameters(false);
            byte[] privateKey = rsaProvider.ExportCspBlob(true);

            return new RsaKey
            {
                PublicKey = new RsaPublicKey
                {
                    Modulus = Convert.ToBase64String(publicParam.Modulus),
                    Exponent = Convert.ToBase64String(publicParam.Exponent),
                },
                PrivateKey = Convert.ToBase64String(privateKey)
            };
        }

        /// <summary>
        /// RSA加密 
        /// </summary>
        /// <param name="text">待加密字符</param>
        /// <param name="publicKey">加密公钥</param>
        public static string Encrypt(string text, RsaPublicKey publicKey)
        {
            RSAParameters param = new RSAParameters()
            {
                Modulus = Convert.FromBase64String(publicKey.Modulus),
                Exponent = Convert.FromBase64String(publicKey.Exponent),
            };

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(KEYSIZE);
            rsa.ImportParameters(param);
            byte[] plainText = Encoding.UTF8.GetBytes(text);
            byte[] cipherBytes = rsa.Encrypt(plainText, false);
            return Convert.ToBase64String(cipherBytes);
        }

        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="text">待解密字符串</param>
        /// <param name="privateKey">解密私钥</param>
        public static string Decrypt(string text, string privateKey)
        {
            byte[] privateKeyBlob = Convert.FromBase64String(privateKey);

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(KEYSIZE);
            rsa.ImportCspBlob(privateKeyBlob);

            byte[] cipherBytes = Convert.FromBase64String(text);
            byte[] plainText = rsa.Decrypt(cipherBytes, false);
            return Encoding.UTF8.GetString(plainText);
        }
    }
}
