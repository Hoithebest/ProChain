using FluentAssertions;
using Utilities.Hashing;
using Xunit;

namespace Utilities.Unit.Tests.Hashing
{
    public class SHA256HasherTests
    {
        [Theory]
        [InlineData("Hello world!", "c0535e4be2b79ffd93291305436bf889314e4a3faec05ecffcbb7df31ad9e51a")]
        [InlineData("Hello World!", "7f83b1657ff1fc53b92dc18148a1d65dfc2d4b1fa3d677284addd200126d9069")]
        [InlineData("test", "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08")]
        public void GetHash_ReturnsSHA256Hash(string input, string expectedOutput)
        {
            string output = SHA256Hasher.GetHash(input);

            output.Should().Be(expectedOutput);
        }
    }
}
