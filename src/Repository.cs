using System;
using System.Collections.Generic;

namespace BlockChain.Demo
{
    public static class Repository
    {
        public static IList<Block> BlockChain = new List<Block>();

        /// <summary>
        /// If the new block chain than the current block chain update, switch the current chain for the latest the block chain.
        /// </summary>
        /// <param name="blockChain">New BlockChain</param>
        public static void SwitchChain(IList<Block> blockChain)
        {
            if (blockChain.Count > BlockChain.Count)
                BlockChain = blockChain;
        }
    }
}
