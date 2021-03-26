using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace GammaLibrary.Extensions
{
    public static class ObjectExtensions
    {
        public static Action<string> Out { get; set; } = Console.WriteLine;
        public static Action<string, string> FormatOut { get; set; } = Console.WriteLine;
        
        public static T Print<T>(this T obj) //TODO customizable to string
        {
            Out(obj.ToStringPlus());
            return obj;
        }

        public static T PrintFormat<T>(this T obj, string format)
        {
            FormatOut(format, obj.ToStringPlus());
            return obj;
        }

        [return: NotNullIfNotNull("obj")]
        public static object? ToObject<T>(this T obj) => obj;

        public static T Run<T>(this T obj, Action<T> action)
        {
            action(obj);
            return obj;
        }

        public static TResult Run<TObject, TResult>(this TObject obj, Func<TObject, TResult> action)
        {
            return action(obj);
        }
    }
}
