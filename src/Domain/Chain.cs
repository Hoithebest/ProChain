using System;
using System.Collections.Generic;

namespace Domain
{
    public class Chain
    {
        private List<Block> _blocks;

        public int Capacity => _blocks.Count;
        public Block LastBlock => _blocks[_blocks.Count - 1];
        public IReadOnlyList<Block> Blocks => _blocks;
        public bool IsValid => IsChainValid();
        public int Difficulty { get; private set; }

        public Chain(int difficulty)
        {
            _blocks = new List<Block>();

            Difficulty = difficulty;
        }

        public void AddBlock(Block block)
        {
            _blocks.Add(block);
        }

        private bool IsChainValid()
        {
            Block currentBlock = null;
            Block previousBlock = null;
            bool isValid = true;
            int index = 1;
            string hashTarget = new string('0', Difficulty);

            while (isValid && index < Capacity)
            {
                currentBlock = _blocks[index];
                previousBlock = _blocks[index - 1];

                if (currentBlock.Hash != currentBlock.CalculateHash() ||
                    previousBlock.Hash != currentBlock.PreviousHash ||
                    currentBlock.Hash.Substring(0, Difficulty) != hashTarget)
                {
                    isValid = false;
                }

                index++;
            }

            return isValid;
        }
    }
}
