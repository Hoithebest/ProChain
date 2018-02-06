using Domain;
using System;
using System.Diagnostics;

namespace Application
{
    class Program
    {
        private const int MiningDifficulty = 5;

        static void Main(string[] args)
        {
            var chain = new Chain(MiningDifficulty);

            AddAndMine(chain, new Block("The first block.", "0"));
            AddAndMine(chain, new Block("The second block.", chain.LastBlock.Hash));
            AddAndMine(chain, new Block("The third block.", chain.LastBlock.Hash));

            Console.WriteLine("Mined all blocks. Here's the chain:");

            ShowChain(chain);

            Console.ReadLine();
        }

        private static void AddAndMine(Chain chain, Block block)
        {
            Stopwatch sw = Stopwatch.StartNew();

            chain.AddBlock(block);

            Console.WriteLine("Added block to the chain. Now mining...");

            block.Mine(MiningDifficulty);

            Console.WriteLine($"Block was mined. Hash is {block.Hash} (done in {sw.Elapsed})");
        }

        private static void ShowChain(Chain chain)
        {
            foreach (Block block in chain.Blocks)
            {
                ShowBlock(block);
            }
        }

        private static void ShowBlock(Block block)
        {
            Console.WriteLine($"{block.Data} - Hash {block.Hash}");
        }
    }
}
