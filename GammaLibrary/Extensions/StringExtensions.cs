﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
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
        public static string ToHexStringWithDash(this byte[] bytes) => BitConverter.ToString(bytes);

        #region numbers

        public static bool IsShort(this string str) => short.TryParse(str, out _);
        public static bool TryConvertToShort(this string str, out short num) => short.TryParse(str, out num);
        public static short ToShort(this string str) => short.Parse(str);

        public static bool IsInt(this string str) => int.TryParse(str, out _);
        public static bool TryConvertToInt(this string str, out int num) => int.TryParse(str, out num);
        public static int ToInt(this string str) => int.Parse(str);

        public static bool IsUInt(this string str) => uint.TryParse(str, out _);
        public static bool TryConvertToUInt(this string str, out uint num) => uint.TryParse(str, out num);
        public static uint ToUInt(this string str) => uint.Parse(str);

        public static bool IsLong(this string str) => long.TryParse(str, out _);
        public static bool TryConvertToLong(this string str, out long num) => long.TryParse(str, out num);
        public static long ToLong(this string str) => long.Parse(str);

        public static bool IsFloat(this string str) => float.TryParse(str, out _);
        public static bool TryConvertToFloat(this string str, out float num) => float.TryParse(str, out num);
        public static float ToFloat(this string str) => float.Parse(str);

        public static bool IsDouble(this string str) => double.TryParse(str, out _);
        public static bool TryConvertToDouble(this string str, out double num) => double.TryParse(str, out num);
        public static double ToDouble(this string str) => double.Parse(str);

        public static bool IsInteger(this string str) => BigInteger.TryParse(str, out _);
        public static bool TryConvertToBigInteger(this string str, out BigInteger num) => BigInteger.TryParse(str, out num);
        public static BigInteger ToBigInteger(this string str) => BigInteger.Parse(str);

        #endregion

        public static string RemovePrefix(this string str, string prefix) => str.StartsWith(prefix) ? str.Substring(prefix.Length) : str;
        public static string RemovePostfix(this string str, string postfix) => str.EndsWith(postfix) ? str.SubStringFromLast(postfix.Length) : str;

        public static Uri ToUri(this string str) => new Uri(str);

        public static string GetFirstPart(this string source, char separator) => source.Split(separator).First();
        public static string GetFirstPart(this string source, string separator) => source.Split(separator).First();

        public static string GetLastPart(this string source, char separator) => source.Split(separator).Last();
        public static string GetLastPart(this string source, string separator) => source.Split(separator).Last();

        public static string[] Split(this string source, string separator, StringSplitOptions options = StringSplitOptions.None) => source.Split(separator.AsArray(), options);

        public static string RemoveFirstChar(this string str) => str.Length > 0 ? str.Substring(1) : str;
        public static string RemoveLastChar(this string str) => str.Length > 0 ? str.SubStringFromLast(1) : str;
        public static string RemoveSurroundChars(this string str, int count = 1) => str.Length >= 2 * count ? str.Substring(count, str.Length - (count + 1)) : string.Empty;

        public static string SubStringFromLast(this string str, int length) => str.Substring(0, str.Length - length);

        public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);
        public static bool IsNullOrWhiteSpace(this string str) => string.IsNullOrWhiteSpace(str);

        public static bool NotNullOrEmpty(this string str) => !string.IsNullOrEmpty(str);
        public static bool NotNullOrWhiteSpace(this string str) => !string.IsNullOrWhiteSpace(str);

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