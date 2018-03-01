using System;
using System.Security.Cryptography;
using System.Text;

namespace BlockChain.Demo
{
    public static class BlockExtensions
    {
        public static string Hash(this Block block)
        {
            var content = $"{block.Index}{block.PrevHash}{block.Timestamp}{block.Data}";
            var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(content));
            var builder = new StringBuilder();

            foreach (byte @byte in bytes)
                builder.Append(@byte.ToString("x2"));

            return builder.ToString();
        }

        public static long Timestamp(this Block block)
        {
            var startTime = TimeZoneInfo.ConvertTimeToUtc(new DateTime(1970, 1, 1, 0, 0, 0, 0));
            var timestamp = (DateTime.Now.Ticks - startTime.Ticks) / 10000;
            return timestamp;
        }

        /// <summary>
        /// Test current block are valid
        /// </summary>
        /// <param name="oldBlock"></param>
        /// <returns></returns>
        public static bool IsValid(this Block block, Block oldBlock)
        {
            if (oldBlock.Index + 1 != block.Index) return false;
            if (oldBlock.Hash != block.PrevHash) return false;
            if (block.Hash() != block.Hash) return false;
            return true;
        }
    }
}
