using System;
using Xunit;

namespace AdunTech.Cryptography.Tests
{
    public class RSATests
    {
        /// <summary>
        /// 1024位
        /// </summary>
        [Fact]
        public void RSA1024()
        {
            var rsaKey = RSA.GenerateKey(); //生成密钥
            string data = "RSA加密字符串";
            string encrypt = RSA.Encrypt(data, rsaKey.PublicKey); //公钥加密
            string decrypt = RSA.Decrypt(encrypt, rsaKey.PrivateKey); //私钥解密
            Assert.Equal(data, decrypt);
        }
    }
}
