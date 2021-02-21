using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;

namespace GammaLibrary.Extensions
{
    public static class TupleExtensions
    {
        public static (T, T) FlipIf<T>(this (T, T) tuple, bool condition)
        {
            return condition ? (tuple.Item2, tuple.Item1) : tuple;
        }

        public static bool TrueForAll<T>(this (T, T) tuple, Predicate<T> predicate)
        {
            return predicate(tuple.Item1) && predicate(tuple.Item2);
        }

        public static T[] ToArray<T>(this ITuple tuple)
        {
            var array = new T[tuple.Length];
            for (var i = 0; i < tuple.Length; i++)
            {
                array[i] = (T)tuple[i]!;
            }

            return array;
        }

        public static ImmutableArray<T> ToImmutableArray<T>(this ITuple tuple)
        {
            var array = ImmutableArray.CreateBuilder<T>(tuple.Length);
            for (var i = 0; i < tuple.Length; i++)
            {
                array[i] = (T)tuple[i]!;
            }

            return array.ToImmutable();
        }

        public static ImmutableList<T> ToImmutableList<T>(this ITuple tuple)
        {
            var array = ImmutableList.CreateBuilder<T>();
            for (var i = 0; i < tuple.Length; i++)
            {
                array[i] = (T)tuple[i]!;
            }

            return array.ToImmutable();
        }

        public static List<T> ToList<T>(this ITuple tuple)
        {
            var list = new List<T>(tuple.Length);
            for (var i = 0; i < tuple.Length; i++)
            {
                list[i] = (T)tuple[i]!;
            }

            return list;
        }
    }
}
