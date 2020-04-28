using System.Text;
using Xunit;

namespace URDashboardLibrary.Tests
{
    public class IDashboardPackageTests
    {
        [Theory]
        [InlineData("")]
        [InlineData("Test")]
        [InlineData("Test\n")]
        public void Pack_ValidateMethod(string command)
        {
            var expected = Encoding.ASCII.GetBytes(command);

            var actual = IDashboardPackage.Pack(command);

            Assert.Equal(expected.Length, actual.Length);

            for(int i = 0; i<expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i]);
            }
        }

        [Theory]
        [InlineData("")]
        [InlineData("Test")]
        [InlineData("Test\n")]
        public void Unpack_ValidateMethod(string command)
        {
            var bytes = Encoding.ASCII.GetBytes(command);

            var expected = Encoding.ASCII.GetString(bytes);

            var actual = IDashboardPackage.Unpack(bytes);

            Assert.Equal(expected.Length, actual.Length);

            Assert.Equal(expected, actual);
        }
    }
}
