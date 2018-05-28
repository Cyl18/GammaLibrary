using System;
using System.Collections.Generic;
using System.Text;

namespace GammaLibrary.Extensions
{
    public static class EnumerableExtensions
    {
        private static readonly Random Rng = new Random();

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
            {
                action(item);
            }
        }

        public static T PickOne<T>(this IList<T> collection)
        {
            return collection[Rng.Next(collection.Count)];
        }

        public static T PickOne<T>(this T[] collection)
        {
            return collection[Rng.Next(collection.Length)];
        }

        public static void Randomize<T>(this IList<T> list)
        {
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = Rng.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static void Randomize<T>(this IList<T> list, Random rng)
        {
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = rng.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static List<T> CloneAndSort<T>(this IEnumerable<T> list)
        {
            var clist = new List<T>(list);
            clist.Sort();
            return clist;
        }

        public static string Join<T>(this IEnumerable<T> enumerable, string separator = ", ", string prefix = "{", string postfix = "}")
        {
            return $"{prefix}{string.Join(separator, enumerable)}{postfix}";
        }

        public static T[] AsArray<T>(this T obj)
        {
            return new[] { obj };
        }

        public static List<T> AsList<T>(this T obj)
        {
            return new List<T> { obj };
        }
    }
}