using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace GammaLibrary.Extensions
{
    public static class StringExtensions
    {
        public static byte[] ToUTF8Bytes(this string str) => Encoding.UTF8.GetBytes(str);
        public static string ToUTF8String(this byte[] bytes) => Encoding.UTF8.GetString(bytes);
        public static byte[] ToASCIIBytes(this string str) => Encoding.ASCII.GetBytes(str);
        public static string ToASCIIString(this byte[] bytes) => Encoding.ASCII.GetString(bytes);
        public static string ToBase64String(this byte[] bytes) => Convert.ToBase64String(bytes);
        public static byte[] ToBase64SourceBytes(this string str) => Convert.FromBase64String(str);

        public static string ToHexString(this byte[] bytes) => BitConverter.ToString(bytes).Replace("-", "");

        public static bool IsInt(this string str) => int.TryParse(str, out _);
        public static bool TryConvertToInt(this string str, out int num) => int.TryParse(str, out num);
        public static int ToInt(this string str) => int.Parse(str);

        public static bool IsDouble(this string str) => double.TryParse(str, out _);
        public static bool TryConvertToDouble(this string str, out double num) => double.TryParse(str, out num);
        public static double ToDouble(this string str) => double.Parse(str);

        public static bool IsInteger(this string str) => BigInteger.TryParse(str, out _);
        public static bool TryConvertToBigInteger(this string str, out BigInteger num) => BigInteger.TryParse(str, out num);
        public static BigInteger ToBigInteger(this string str) => BigInteger.Parse(str);

        public static string CutPrefix(this string str, string prefix) => str.StartsWith(prefix) ? str.Substring(prefix.Length) : str;

        public static Uri ToUri(this string str) => new Uri(str);

        public static string GetFirstPart(this string source, char separator) => source.Split(separator).First();
        public static string GetFirstPart(this string source, string separator) => source.Split(source.AsArray(), StringSplitOptions.None).First();

        public static string GetLastPart(this string source, char separator) => source.Split(separator).Last();
        public static string GetLastPart(this string source, string separator) => source.Split(source.AsArray(), StringSplitOptions.None).Last();

        public static string[] Split(this string source, string separator) => source.Split(source.AsArray(), StringSplitOptions.None);

        public static string RemoveFirstChar(this string str) => str.Substring(1);
        public static string RemoveLastChar(this string str) => str.Substring(0, str.Length - 1);

        public static string RemoveLastChars(this string str, int count) => str.Substring(0, str.Length - count);
    }
}