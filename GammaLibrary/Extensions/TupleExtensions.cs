using System;
using System.Collections.Generic;
using System.Text;

namespace GammaLibrary.Extensions
{
    public static class TupleExtensions
    {
        public static (T, T) FlipIf<T>(this (T, T) tuple, bool condition)
        {
            return condition ? (tuple.Item2, tuple.Item1) : tuple;
        }
    }
}
