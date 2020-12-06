using System;
using System.Collections.Generic;
using System.Text;

namespace GammaLibrary.Extensions
{
    // 我是不是用了nullable就不用写null check了?
    public static class Unsorted
    {
        public static T2? Do<T1, T2>(this T1? source, Func<T1?, T2?> func)
        {
            if (func is null) throw new ArgumentNullException(nameof(func));
            return func(source);
        }

        public static void DoIfNotNull<T1>(this T1? source, Action<T1?> func)
        {
            if (func == null) throw new ArgumentNullException(nameof(func));

            if (source != null) func(source);
        }

        public static T2? DoIfNotNull<T1, T2>(this T1? source, Func<T1?, T2?> func)
        {
            if (func is null) throw new ArgumentNullException(nameof(func));

            return source != null ? func(source) : default;
        }

        public static void DoIfNull<T1>(this T1? source, Action<T1?> func)
        {
            if (func is null) throw new ArgumentNullException(nameof(func));

            if (source == null) func(source);
        }

        public static T2? DoIfNull<T1, T2>(this T1? source, Func<T1?, T2?> func)
        {
            if (func is null) throw new ArgumentNullException(nameof(func));

            return source == null ? func(source) : default;
        }
    }
}
