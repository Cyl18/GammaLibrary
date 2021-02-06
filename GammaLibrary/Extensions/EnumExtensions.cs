using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GammaLibrary.Extensions
{
    // from https://stackoverflow.com/a/407325/12938355
    public static class EnumExtensions
    {
        // should support long/short etc.
        public static T Append<T>(this Enum type, T value) where T : struct, Enum
        {
            return (T)(ValueType)((int)(ValueType)type | (int)(ValueType)value);
        }

        public static T Remove<T>(this Enum type, T value) where T : struct, Enum
        {
            return (T)(ValueType)((int)(ValueType)type & ~(int)(ValueType)value);
        }

        public static bool Has<T>(this Enum type, T value) where T : struct, Enum
        {
            return ((int)(ValueType)type & (int)(ValueType)value) == (int)(ValueType)value;
        }

    }
}
