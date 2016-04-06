using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CSIRC
{
    public static class StringExtension
    {
        public static IEnumerable<string> ChunkSplit(this string str, int chunkSize)
        {
            if (String.IsNullOrWhiteSpace(str))
                throw new ArgumentNullException(nameof(str));
            if (chunkSize < 0)
                throw new ArgumentException(nameof(chunkSize));
            if (str.Length < chunkSize)
                chunkSize = str.Length;
            var split = new List<string>();
            split.AddRange(Enumerable.Range(0, str.Length / chunkSize)
                           .Select(i => str.Substring(i * chunkSize, chunkSize)));
            split.Add(str.Length % chunkSize > 0
                      ? str.Substring((str.Length / chunkSize) * chunkSize, str.Length - ((str.Length / chunkSize) * chunkSize))
                      : string.Empty);
            return split;

        }
    }
}

