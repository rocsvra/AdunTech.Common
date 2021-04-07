using Xunit;

namespace AdunTech.AutoMapperExtension.Tests
{
    public class AutoMapperTest
    {
        [Fact]
        public void Test1()
        {
            var source = new A
            {
                A1 = "1",
                A2 = "2",
                A3 = "3",
                A4 = new D
                {
                    B1 = "11",
                    B2 = "22"
                },
                A6 = new E
                {
                    B1 = "ss",
                    B2 = "dd",
                    B3 = "ff"
                }
            };
            B destination = source.Map<A, B>();
            Assert.Equal("1", destination.A1);
            Assert.Equal("2", destination.A2);
            Assert.Equal("3", destination.A3);
            Assert.Equal("11", destination.A4.B1);
            Assert.Null(destination.A5);
        }
    }

    class A
    {
        public string A1 { get; set; }
        public string A2 { get; set; }
        public string A3 { get; set; }
        public D A4 { get; set; }
        public E A6 { get; set; }
    }

    class B
    {
        public string A1 { get; set; }
        public string A2 { get; set; }
        public string A3 { get; set; }
        public D A4 { get; set; }
        public E A5 { get; set; }
    }

    class D
    {
        public string B1 { get; set; }

        public string B2 { get; set; }
    }

    class E
    {
        public string B1 { get; set; }
        public string B2 { get; set; }
        public string B3 { get; set; }
    }
}
