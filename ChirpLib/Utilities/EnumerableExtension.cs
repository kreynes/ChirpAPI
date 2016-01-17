using System;
using System.Linq;
using System.Collections.Generic;

namespace ChirpLib
{
    public static class EnumerableExtension
    {
        /// <summary>
        /// Splits the string to equal chunks.
        /// </summary>
        /// <returns>Array of string with the size of chunkLength.</returns>
        /// <param name="chunkLength">Size of the chunks.</param>
        public static IEnumerable<string> SplitBy(this string value, int chunkSize)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentNullException("Cannot split null or empty string");
            if (chunkSize < 1)
                throw new ArgumentOutOfRangeException("Invalid chunk length size");
            for (int i = 0; i < value.Length; i += chunkSize)
            {
                if (chunkSize + i > value.Length)
                    chunkSize = value.Length - 1;
                yield return value.Substring(i, chunkSize);
            }
        }
        public static bool IsNullOrEmpty(this string[] value)
        {
            if (value == null || value.Length < 1)
                return false;
            else
                return true;
        }
    }
}

