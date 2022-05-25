using System;
using System.ComponentModel;
using Xunit;

namespace AdunTech.Extension.Tests
{
    public class EnumTests
    {
        [Fact]
        public void Test1()
        {
            string desc = SexEnum.Male.EnumDescription();
            string name = SexEnum.Male.EnumName();

            Assert.Equal("ÄÐ", desc);
            Assert.Equal("Male", name);
        }
    }

    public enum SexEnum
    {
        [Description("ÄÐ")]
        Male,
        [Description("Å®")]
        Female
    }
}
