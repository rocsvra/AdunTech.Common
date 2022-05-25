using AdunTech.Co2Net.Helpers;
using Xunit;

namespace AdunTech.Co2Net.Tests
{
    public class RandomCodeTests
    {
        [Fact]
        public void Test1()
        {
            var id = RandomCodeHelper.GenerateSequentialId();
            var idNext = RandomCodeHelper.GenerateSequentialId();
            int sort = string.Compare(id, idNext);
            Assert.True(sort < 0);
        }
    }
}