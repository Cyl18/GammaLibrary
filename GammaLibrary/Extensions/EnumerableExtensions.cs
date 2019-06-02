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

        public static IEnumerable<T> DoForAll<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
            {
                action(item);
                yield return item;
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

        public static T PickOne<T>(this IList<T> collection, Random rng)
        {
            return collection[rng.Next(collection.Count)];
        }

        public static T PickOne<T>(this T[] collection, Random rng)
        {
            return collection[rng.Next(collection.Length)];
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
            var result = new List<T>(list);
            result.Sort();
            return result;
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

        public static T[] AsArray<T>(this (T, T) obj)
        {
            var (item1, item2) = obj;
            return new [] { item1, item2 };
        }

        public static List<T> AsList<T>(this (T, T) obj)
        {
            var (item1, item2) = obj;
            return new List<T> { item1, item2 };
        }

        public static T[] AsArray<T>(this (T, T, T) obj)
        {
            var (item1, item2, item3) = obj;
            return new[] { item1, item2, item3 };
        }

        public static List<T> AsList<T>(this (T, T, T) obj)
        {
            var (item1, item2, item3) = obj;
            return new List<T> { item1, item2, item3 };
        }

        public static bool TryGet<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, out TValue value)
        {
            var flag = dictionary.ContainsKey(key);
            value = flag ? dictionary[key] : default;
            return flag;
        }

        public static (bool success, TValue value) SafelyGet<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
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

        public static (bool success, TValue value) SafelyGet<TValue>(this IList<TValue> list, int index)
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

        public static (bool success, TValue value) SafelyGet<TValue>(this TValue[] list, int index)
        {
            var flag = list.Length <= index;
            var value = flag ? list[index] : default;
            return (flag, value);
        }

        public static bool IsEmpty<T>(this IEnumerable<T> enumerable) => !enumerable.Any();
    }
}