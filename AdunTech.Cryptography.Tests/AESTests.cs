using Xunit;

namespace AdunTech.Cryptography.Tests
{
    public class AESTests
    {
        /// <summary>
        /// 256λ����
        /// </summary>
        [Fact]
        public void AES256()
        {
            string data = "AES�����ַ���";
            string key = AES.GenerateKey256(); //��Կ
            string encrypt = AES.Encrypt256(data, key); //���ܴ�
            string decrypt = AES.Decrypt256(encrypt, key); //���ܴ�
            Assert.Equal(data, decrypt);
        }

        /// <summary>
        /// ������-128λ����
        /// </summary>
        [Fact]
        public void AES128()
        {
            string data = "AES�����ַ���";
            string key = AES.GenerateKey128();
            string iv = AES.GenerateIV128();
            string encrypt = AES.Encrypt128(data, key, iv);
            string decrypt = AES.Decrypt128(encrypt, key, iv);
            Assert.Equal(data, decrypt);
        }
    }
}
