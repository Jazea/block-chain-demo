namespace BlockChain.Demo
{
    public class Block
    {
        public Block()
        {
            Index = 0;
            Timestamp = this.Timestamp();
            Data = null;
            PrevHash = null;
            Hash = this.Hash();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="block">Old block</param>
        /// <param name="data"></param>
        public Block(Block block, byte[] data)
        {
            Index = block.Index + 1;
            Timestamp = this.Timestamp();
            Data = data;
            PrevHash = block.Hash;
            Hash = this.Hash();
        }
        /// <summary>
        /// Block position
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// Block to generate a timestamp
        /// </summary>
        public long Timestamp { get; }

        /// <summary>
        /// Block Data
        /// </summary>
        public byte[] Data { get; }

        /// <summary>
        /// Block SHA-256 Hash value
        /// </summary>
        public string Hash { get; }

        /// <summary>
        /// Previous block SHA-256 hash value
        /// </summary>
        public string PrevHash { get; }

    }
}
