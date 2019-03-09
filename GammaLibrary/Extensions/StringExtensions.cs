﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security;
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

        public static bool IsLong(this string str) => long.TryParse(str, out _);
        public static bool TryConvertToLong(this string str, out long num) => long.TryParse(str, out num);
        public static long ToLong(this string str) => long.Parse(str);

        public static bool IsDouble(this string str) => double.TryParse(str, out _);
        public static bool TryConvertToDouble(this string str, out double num) => double.TryParse(str, out num);
        public static double ToDouble(this string str) => double.Parse(str);

        public static bool IsInteger(this string str) => BigInteger.TryParse(str, out _);
        public static bool TryConvertToBigInteger(this string str, out BigInteger num) => BigInteger.TryParse(str, out num);
        public static BigInteger ToBigInteger(this string str) => BigInteger.Parse(str);

        public static string RemovePrefix(this string str, string prefix) => str.StartsWith(prefix) ? str.Substring(prefix.Length) : str;
        //public static string RemovePrefix(this string str, string prefix) => str.StartsWith(prefix) ? str.Substring(prefix.Length) : str;
        //TODO

        public static Uri ToUri(this string str) => new Uri(str);

        public static string GetFirstPart(this string source, char separator) => source.Split(separator).First();
        public static string GetFirstPart(this string source, string separator) => source.Split(separator).First();

        public static string GetLastPart(this string source, char separator) => source.Split(separator).Last();
        public static string GetLastPart(this string source, string separator) => source.Split(separator).Last();

        public static string[] Split(this string source, string separator) => source.Split(separator.AsArray(), StringSplitOptions.None);

        public static string RemoveFirstChar(this string str) => str.Substring(1);
        public static string RemoveLastChar(this string str) => str.Substring(0, str.Length - 1);
        public static string SubStringFromLast(this string str, int length) => str.Substring(0, str.Length - length);

        public static string RemoveSurroundChar(this string str) => str.Substring(1, str.Length - 2);

        public static string RemoveLastChars(this string str, int count) => str.Substring(0, str.Length - count);

        public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);
        public static bool IsNullOrWhiteSpace(this string str) => string.IsNullOrWhiteSpace(str);

        public static string GetString(this SecureString value)
        {
            var valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }
    }
}