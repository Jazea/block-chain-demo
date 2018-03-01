namespace BlockChain.Demo
{
    public class Block
    {
        public Block()
        {
            Index = 0;
            Timestamp = this.GetTimestamp();
            Data = null;
            PrevHash = null;
            Hash = this.GetHash();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="block">Old block</param>
        /// <param name="data"></param>
        public Block(Block block, string data)
        {
            Index = block.Index + 1;
            Timestamp = this.GetTimestamp();
            Data = data;
            PrevHash = block.Hash;
            Hash = this.GetHash();
        }
        /// <summary>
        /// Block position
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Block to generate a timestamp
        /// </summary>
        public long Timestamp { get; set; }

        /// <summary>
        /// Block Data
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Block SHA-256 Hash value
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        /// Previous block SHA-256 hash value
        /// </summary>
        public string PrevHash { get; set; }

    }
}
