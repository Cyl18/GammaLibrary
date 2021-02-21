using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.CodeAnalysis;

namespace GammaLibrary.Enhancements
{
    public static class Arrays<T>
    {
        public static T[] Empty = Array.Empty<T>();
        public static List<T> EmptyList => new();
    }
}
