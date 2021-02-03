using Xunit;

namespace AdunTech.Cryptography.Tests
{
    public class AESTests
    {
        /// <summary>
        /// 256位加密
        /// </summary>
        [Fact]
        public void AES256()
        {
            string data = "AES加密字符串";
            string key = AES.GenerateKey256(); //密钥
            string encrypt = AES.Encrypt256(data, key); //加密串
            string decrypt = AES.Decrypt256(encrypt, key); //解密串
            Assert.Equal(data, decrypt);
        }

        /// <summary>
        /// 带向量-128位加密
        /// </summary>
        [Fact]
        public void AES128()
        {
            string data = "AES加密字符串";
            string key = AES.GenerateKey128();
            string iv = AES.GenerateIV128();
            string encrypt = AES.Encrypt128(data, key, iv);
            string decrypt = AES.Decrypt128(encrypt, key, iv);
            Assert.Equal(data, decrypt);
        }
    }
}
