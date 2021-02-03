using Xunit;

namespace AdunTech.Cryptography.Tests
{
    public class DESTests
    {
        /// <summary>
        /// DES加密
        /// </summary>
        [Fact]
        public void Test()
        {
            string data = "DES加密字符串";

            DesKey desKey = DES.GenerateKey();
            string key = desKey.Key; //密钥
            string iv = desKey.IV; //辅助向量
            string encrypt = DES.Encrypt(data, key, iv);
            string decrypt = DES.Decrypt(encrypt, key, iv);

            Assert.Equal(data, decrypt);
        }
    }
}
