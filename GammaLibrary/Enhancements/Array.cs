using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.CodeAnalysis;

namespace GammaLibrary.Enhancements
{
    internal static class Arrays<T>
    {
        public static T[] Empty = Array.Empty<T>();

        [SuppressMessage("Design", "CA1002:Do not expose generic lists", Justification = "<Pending>")]
        public static List<T> EmptyList => new();
    }
}
