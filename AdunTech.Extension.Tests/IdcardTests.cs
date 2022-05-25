using Xunit;

namespace AdunTech.Extension.Tests
{
    public class IdcardTests
    {
        [Fact]
        public void Test1()
        {
            string idcard = "330225198201084812";
            string birth = idcard.GetBirthday()?.ToString("yyyy-MM-dd");
            int age = idcard.GetAge();
            int sex = idcard.GetSex();

            Assert.Equal("1982-01-08", birth);
            Assert.Equal(39, age);
            Assert.Equal(1, sex);
        }
    }
}
