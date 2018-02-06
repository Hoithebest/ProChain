using FluentAssertions;
using Xunit;

namespace Domain.Unit.Tests
{
    public class ChainTests
    {
        private Chain CreateSut()
        {
            return new Chain(1);
        }

        [Fact]
        public void AddBlock_ShouldAddBlockToTheChain()
        {
            Chain chain = CreateSut();

            chain.AddBlock(new Block("The first block.", "0"));
            chain.AddBlock(new Block("The second block.", chain.LastBlock.Hash));

            chain.Capacity.Should().Be(2);
        }

        [Fact]
        public void LastBlock_ShouldReturnLastAddedBlock()
        {
            Chain chain = CreateSut();

            var genesisBlock = new Block("The first block.", "0");

            chain.AddBlock(genesisBlock);

            chain.LastBlock.Should().Be(genesisBlock);

            var secondBlock = new Block("The second block.", genesisBlock.Hash);

            chain.AddBlock(secondBlock);

            chain.LastBlock.Should().Be(secondBlock);
        }

        [Fact]
        public void IsChainValid_WhenChainIsNotValid_ReturnsFalse()
        {
            Chain chain = CreateSut();

            chain.AddBlock(new Block("The first block.", "0"));
            chain.AddBlock(new Block("The second block.", chain.LastBlock.Hash));
            chain.AddBlock(new Block("The third block.", chain.LastBlock.Hash));
            chain.AddBlock(new Block("The fourth block.", chain.Blocks[0].Hash));
            chain.AddBlock(new Block("The fifth block.", chain.LastBlock.Hash));

            chain.IsValid.Should().BeFalse();
        }

        [Fact]
        public void IschainValid_WhenChainIsValidButNotMined_ReturnsFalse()
        {
            Chain chain = CreateSut();

            chain.AddBlock(new Block("The first block.", "0"));
            chain.AddBlock(new Block("The second block.", chain.LastBlock.Hash));
            chain.AddBlock(new Block("The third block.", chain.LastBlock.Hash));
            chain.AddBlock(new Block("The fourth block.", chain.LastBlock.Hash));
            chain.AddBlock(new Block("The fifth block.", chain.LastBlock.Hash));

            chain.IsValid.Should().BeFalse();
        }

        [Fact]
        public void IschainValid_WhenChainIsValidAndMined_ReturnsTrue()
        {
            Chain chain = CreateSut();

            chain.AddBlock(new Block("The first block.", "0"));
            chain.Blocks[0].Mine(chain.Difficulty);
            chain.AddBlock(new Block("The second block.", chain.LastBlock.Hash));
            chain.Blocks[1].Mine(chain.Difficulty);
            chain.AddBlock(new Block("The third block.", chain.LastBlock.Hash));
            chain.Blocks[2].Mine(chain.Difficulty);
            chain.AddBlock(new Block("The fourth block.", chain.LastBlock.Hash));
            chain.Blocks[3].Mine(chain.Difficulty);
            chain.AddBlock(new Block("The fifth block.", chain.LastBlock.Hash));
            chain.Blocks[4].Mine(chain.Difficulty);

            chain.IsValid.Should().BeTrue();
        }
    }
}
