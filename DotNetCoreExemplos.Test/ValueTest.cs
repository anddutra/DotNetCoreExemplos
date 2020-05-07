using DotNetCoreExemplos.Services;
using Xunit;

namespace DotNetCoreExemplos.Test
{
    public class ValueTest
    {

        private ValueServices _valueServices;

        public ValueTest()
        {
            _valueServices = new ValueServices(null);
        }

        [Fact]
        public void TestGetRandomValue()
        {
            int value = _valueServices.GetRandomValue();

            Assert.True(value > 0 && value <= 100);
        }

        [Fact]
        public void TestGetNextValue()
        {
            int value1 = _valueServices.GetNextValue();
            int value2 = _valueServices.GetNextValue();
            int value3 = _valueServices.GetNextValue();

            Assert.Equal(1, value1);
            Assert.Equal(2, value2);
            Assert.Equal(3, value3);
        }

        [Theory]
        [InlineData(34, "Número par")]
        [InlineData(77, "Número impar")]
        public void TestGetOddOrEven(int value, string result)
        {
            string oddOrEven = _valueServices.GetOddOrEven(value);

            Assert.Equal(result, oddOrEven);
        }

        [Theory]
        [InlineData(20, 10, 50, 80)]
        [InlineData(15, 5, 20, 40)]
        public void TestGetSum3Values(int value1, int value2, int value3, int result)
        {
            int GetSum3Values = _valueServices.GetSum3Values(value1, value2, value3);

            Assert.Equal(result, GetSum3Values);
        }
    }
}
