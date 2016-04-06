using System;
namespace CSIRC
{
    public static class StringArrayExtensions
    {
        public static bool IsNullOrEmpty(this Array array)
        {
            return (array == null || array.Length == 0);
        }
    }
}

