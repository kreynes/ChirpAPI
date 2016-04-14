using System;
namespace ChirpAPI
{
    public static class StringArrayExtensions
    {
        public static bool IsNullOrEmpty(this Array array)
        {
            return (array == null || array.Length == 0);
        }
    }
}

