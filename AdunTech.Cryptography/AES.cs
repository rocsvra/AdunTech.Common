using System;
using System.Security.Cryptography;
using System.Text;

namespace AdunTech.Cryptography
{
    /// <summary>
    /// AES加密
    /// Advanced Encryption Standard，高级加密标准
    /// </summary>
    public class AES
    {
        /// <summary>
        /// 256位密钥
        /// </summary>
        /// <returns></returns>
        public static string GenerateKey256()
        {
            return Generator.GenerateKey(256);
        }

        /// 128位密钥
        /// </summary>
        /// <returns></returns>
        public static string GenerateKey128()
        {
            return Generator.GenerateKey(128);
        }

        /// <summary>
        /// 128位向量
        /// </summary>
        /// <returns></returns>
        public static string GenerateIV128()
        {
            return Generator.GenerateIV(128);
        }

        /// <summary>
        /// <summary>
        /// 加密(256位)
        /// </summary>
        /// <param name="text">明文（待加密）</param>
        /// <param name="key">秘钥，32个字符，每个字符只能占1Byte（非中文）</param>
        /// <returns></returns>
        public static string Encrypt256(string text, string key)
        {
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(text);

            RijndaelManaged rm = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = rm.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        ///  AES 解密(256位)
        /// </summary>
        /// <param name="text">明文（待解密）</param>
        /// <param name="key">秘钥</param>
        /// <returns></returns>
        public static string Decrypt256(string text, string key)
        {
            byte[] toEncryptArray = Convert.FromBase64String(text);

            RijndaelManaged rm = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = rm.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Encoding.UTF8.GetString(resultArray);
        }

        /// <summary>
        /// AES加密(128位)
        /// </summary>
        /// <param name="text">明文（待加密）</param>
        /// <param name="key">秘钥</param>
        /// <param name="iv">加密辅助向量</param>
        /// <returns></returns>
        public static string Encrypt128(string text, string key, string iv)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.PKCS7;
            rijndaelCipher.KeySize = 128;
            rijndaelCipher.BlockSize = 128;
            byte[] pwdBytes = Encoding.UTF8.GetBytes(key);
            byte[] keyBytes = new byte[16];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length) len = keyBytes.Length;
            Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            byte[] ivBytes = Encoding.UTF8.GetBytes(iv);
            rijndaelCipher.IV = ivBytes;
            ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
            byte[] plainText = Encoding.UTF8.GetBytes(text);
            byte[] cipherBytes = transform.TransformFinalBlock(plainText, 0, plainText.Length);
            return Convert.ToBase64String(cipherBytes);
        }

        /// <summary>
        /// AES解密(128位)
        /// </summary>
        /// <param name="text">明文（待解密）</param>
        /// <param name="key">秘钥</param>
        /// <param name="iv">解密辅助向量</param>
        /// <returns></returns>
        public static string Decrypt128(string text, string key, string iv)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.PKCS7;
            rijndaelCipher.KeySize = 128;
            rijndaelCipher.BlockSize = 128;
            byte[] encryptedData = Convert.FromBase64String(text);
            byte[] pwdBytes = Encoding.UTF8.GetBytes(key);
            byte[] keyBytes = new byte[16];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length) len = keyBytes.Length;
            Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            byte[] ivBytes = Encoding.UTF8.GetBytes(iv);
            rijndaelCipher.IV = ivBytes;
            ICryptoTransform transform = rijndaelCipher.CreateDecryptor();
            byte[] plainText = transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
            return Encoding.UTF8.GetString(plainText);
        }
    }
}
