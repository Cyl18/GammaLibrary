﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace GammaLibrary.Extensions
{
    public static class EnumerableExtensions
    {
        static readonly ThreadLocal<Random> Rng = new(() => new Random());

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
            {
                action(item);
                yield return item;
            }
        }

        public static bool IsIn<T>(this T obj, IEnumerable<T> array)
        {
            return array.Contains(obj);
        }

        /// <summary>
        /// Determines whether an element is not in the <see cref="List{T}"/>
        /// </summary>
        /// <param name="list">The source <see cref="List{T}"/></param>
        /// <param name="item">The object to locate in the <see cref="List{T}" />. The value can be <see langword="null" /> for reference types.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="item"/> is not found in the <see cref="List{T}"/>;
        /// otherwise, <see langword="false"/>
        /// </returns>
        public static bool NotContains<T>(this IList<T> list, T item) => !list.Contains(item);
        // todo 移除这个方法因为 IEnumerable 已经包含

        public static bool NotContains<T>(this ICollection<T> collection, T obj) => !collection.Contains(obj);

        public static bool NotContains<T>(this IEnumerable<T> list, T obj, IEqualityComparer<T> eq) =>
            !list.Contains(obj, eq);

        public static bool NotContains<T>(this IEnumerable<T> enumerable, T obj) => !enumerable.Contains(obj);

        /// <summary>
        /// Randomly pick an element from a <paramref name="collection"/> 
        /// </summary>
        public static T PickOne<T>(this IList<T> collection)
        {
            return collection[Rng.Value!.Next(collection.Count)];
        }

        /// <summary>
        /// Randomly pick an element from a <paramref name="collection"/>
        /// </summary>
        public static T PickOne<T>(this T[] collection)
        {
            return collection[Rng.Value!.Next(collection.Length)];
        }

        /// <summary>
        /// Randomly pick an element from an <paramref name="enumerable"/>
        /// </summary>
        /// <remarks>
        /// WARNING: This method calls <c>ToArray()</c>, so it's <c>SUPER INEFFICIENT</c>.
        /// </remarks>
        public static T PickOne<T>(this IEnumerable<T> enumerable)
        {
            var collection = enumerable.ToArray();
            return collection[Rng.Value!.Next(collection.Length)];
        }

        /// <summary>
        /// Randomly pick an element from an <paramref name="enumerable"/> using specified <paramref name="rng"/>
        /// </summary>
        /// <remarks>
        /// WARNING: This method calls <c>ToArray()</c>, so it's <c>SUPER INEFFICIENT</c>.
        /// </remarks>
        public static T PickOne<T>(this IEnumerable<T> enumerable, Random rng)
        {
            var collection = enumerable.ToArray();
            return collection[rng.Next(collection.Length)];
        }

        /// <summary>
        /// Randomly pick an element from a <paramref name="collection"/> using specified <paramref name="rng"/>
        /// </summary>
        public static T PickOne<T>(this IList<T> collection, Random rng)
        {
            return collection[rng.Next(collection.Count)];
        }

        /// <summary>
        /// Randomly pick an element from a <paramref name="collection"/> using specified <paramref name="rng"/>
        /// </summary>
        public static T PickOne<T>(this T[] collection, Random rng)
        {
            return collection[rng.Next(collection.Length)];
        }

        // todo find a better algorithm
        // todo !这里排序算法有问题
        /// <summary>
        /// Shuffles a <paramref name="list"/>.
        /// </summary>
        public static void Shuffle<T>(this IList<T> list)
        {
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = Rng.Value!.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        /// <summary>
        /// Shuffles a <paramref name="list"/> using specified <paramref name="rng"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="rng"></param>
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

#pragma warning disable CA1002 // Do not expose generic lists
        public static List<T> CloneAndSort<T>(this IEnumerable<T> list)
        {
            var result = new List<T>(list);
            result.Sort();
            return result;
        }

        public static string Connect<T>(this IEnumerable<T> enumerable, string separator = ", ", string prefix = "",
            string postfix = "")
        {
            return $"{prefix}{string.Join(separator, enumerable)}{postfix}";
        }

        public static T[] AsArray<T>(this T obj)
        {
            return new[] {obj};
        }

        public static List<T> AsList<T>(this T obj)
        {
            return new List<T> {obj};
        }

        public static T[] AsArray<T>(this (T, T) obj)
        {
            var (item1, item2) = obj;
            return new[] {item1, item2};
        }

        public static List<T> AsList<T>(this (T, T) obj)
        {
            var (item1, item2) = obj;
            return new List<T> {item1, item2};
        }

        public static T[] AsArray<T>(this (T, T, T) obj)
        {
            var (item1, item2, item3) = obj;
            return new[] {item1, item2, item3};
        }

        public static List<T> AsList<T>(this (T, T, T) obj)
        {
            var (item1, item2, item3) = obj;
            return new List<T> {item1, item2, item3};
        }

        public static T[] AsArray<T>(this (T, T, T, T) obj)
        {
            var (item1, item2, item3, item4) = obj;
            return new[] {item1, item2, item3, item4};
        }

        public static List<T> AsList<T>(this (T, T, T, T) obj)
        {
            var (item1, item2, item3, item4) = obj;
            return new List<T> {item1, item2, item3, item4};
        }

        public static TValue? GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            dictionary.TryGetValue(key, out var value);
            return value;
        }

        public static TValue GetOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> directory, TKey key,
            Func<TValue>? creator = null) where TValue : new()
        {
            if (!directory.ContainsKey(key)) directory[key] = creator == null ? new TValue() : creator();
            return directory[key];
        }


        public static bool TryGet<TValue>(this IList<TValue> list, int index, out TValue? value)
        {
            var flag = list.Count <= index;
            value = flag ? list[index] : default;
            return flag;
        }

        public static bool TryGet<TValue>(this TValue[] list, int index, out TValue? value)
        {
            var flag = list.Length <= index;
            value = flag ? list[index] : default;
            return flag;
        }

        public static TValue? GetOrDefault<TValue>(this IList<TValue> list, int index)
        {
            list.TryGet(index, out var value);
            return value;
        }

        public static TValue? GetOrDefault<TValue>(this TValue[] list, int index)
        {
            list.TryGet(index, out var value);
            return value;
        }

        public static IEnumerable<T> DistinctByX<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property)
        {
            return items.GroupBy(property).Select(x => x.First());
        }

        // IEnumerable<T>.Any 已经针对 IList<T>/T[] 优化
        public static bool IsEmpty<T>(this IEnumerable<T> enumerable) => !enumerable.Any();
    }
}
