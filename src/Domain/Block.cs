using System;
using Utilities.Hashing;
using Utilities.Timing;

namespace Domain
{
    public class Block
    {
        public string Hash { get; private set; }
        public string PreviousHash { get; private set; }
        public string Data { get; private set; }
        public long TimeStamp { get; private set; }

        private int nonce;

        public Block(string data, string previousHash)
        {
            Data = data;
            PreviousHash = previousHash;
            TimeStamp = GetTimeStamp();
            Hash = CalculateHash();
        }

        private long GetTimeStamp()
        {
            return DateTime.UtcNow.GetTimestamp();
        }

        public string CalculateHash()
        {
            return SHA256Hasher.GetHash(PreviousHash + TimeStamp.ToString() + nonce.ToString() + Data);
        }

        public void Mine(int difficulty)
        {
            string target = new string('0', difficulty);

            while (Hash.Substring(0, difficulty) != target)
            {
                nonce++;

                Hash = CalculateHash();
            }
        }
    }
}
