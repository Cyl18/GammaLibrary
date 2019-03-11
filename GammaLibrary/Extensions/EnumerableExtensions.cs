using System;
using System.Collections.Generic;
using System.Linq;
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

        public static void Shuffle<T>(this IList<T> list)
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

        public static void Shuffle<T>(this IList<T> list, Random rng)
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

        public static string Connect<T>(this IEnumerable<T> enumerable, string separator = ", ", string prefix = "", string postfix = "")
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

        public static bool TryGet<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, out TValue value)
        {
            var flag = dictionary.ContainsKey(key);
            value = flag ? dictionary[key] : default;
            return flag;
        }

        public static (bool success, TValue value) SafeGet<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            var flag = dictionary.ContainsKey(key);
            var value = flag ? dictionary[key] : default;
            return (flag, value);
        }

        public static bool TryGet<TValue>(this IList<TValue> list, int index, out TValue value)
        {
            var flag = list.Count <= index;
            value = flag ? list[index] : default;
            return flag;
        }

        public static (bool success, TValue value) SafeGet<TValue>(this IList<TValue> list, int index)
        {
            var flag = list.Count <= index;
            var value = flag ? list[index] : default;
            return (flag, value);
        }

        public static bool TryGet<TValue>(this TValue[] list, int index, out TValue value)
        {
            var flag = list.Length <= index;
            value = flag ? list[index] : default;
            return flag;
        }

        public static (bool success, TValue value) SafeGet<TValue>(this TValue[] list, int index)
        {
            var flag = list.Length <= index;
            var value = flag ? list[index] : default;
            return (flag, value);
        }

        public static bool Empty<T>(this IEnumerable<T> enumerable) => !enumerable.Any();
    }
}