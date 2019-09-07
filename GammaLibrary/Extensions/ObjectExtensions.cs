using System;
using System.Collections.Generic;
using System.Text;

namespace GammaLibrary.Extensions
{
    public static class ObjectExtensions
    {
        public static Action<string> Printer { get; set; } = Console.WriteLine;
        public static Action<string, string> FormatPrinter { get; set; } = Console.WriteLine;

        public static T Print<T>(this T obj) //TODO customizable to string
        {
            Printer(obj.ToStringX());
            return obj;
        }

        public static T PrintFormat<T>(this T obj, string format)
        {
            FormatPrinter(format, obj.ToStringX());
            return obj;
        }
    }
}
