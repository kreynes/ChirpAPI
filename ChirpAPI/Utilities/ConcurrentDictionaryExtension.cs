using System;
using System.Collections.Concurrent;

namespace ChirpAPI
{
    static class ConcurrentDictionaryExtension
    {
        public static bool TryRemove<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue dummy;
            return dictionary.TryRemove(key, out dummy);
        }
    }
}

