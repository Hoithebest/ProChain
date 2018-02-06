using FluentAssertions;
using Xunit;

namespace Domain.Unit.Tests
{
    public class BlockTests
    {
        [Fact]
        public void CreateNewBlocks_CorrectValuesSet()
        {
            var genesisBlock = new Block("The first block.", "0");
            var secondBlock = new Block("The second block.", genesisBlock.Hash);
            var thirdBlock = new Block("The third block.", secondBlock.Hash);

            secondBlock.PreviousHash.Should().Be(genesisBlock.Hash);
        }
    }
}
