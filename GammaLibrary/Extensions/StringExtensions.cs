using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Text.Json;

namespace GammaLibrary.Extensions
{ 
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "<Pending>")]
    public static class StringExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static byte[] ToBytes(this string str, Encoding encoding) => encoding.GetBytes(str);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static string ConvertToString(this byte[] bytes, Encoding encoding) => encoding.GetString(bytes);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static byte[] ToUTF8Bytes(this string str) => str.ToBytes(Encoding.UTF8);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static string ToUTF8String(this byte[] bytes) => bytes.ConvertToString(Encoding.UTF8);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static byte[] ToUTF32Bytes(this string str) => str.ToBytes(Encoding.UTF32);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static string ToUTF32String(this byte[] bytes) => bytes.ConvertToString(Encoding.UTF32);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static byte[] ToUnicodeBytes(this string str) => str.ToBytes(Encoding.Unicode);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static string ToUnicodeString(this byte[] bytes) => bytes.ConvertToString(Encoding.Unicode);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static byte[] ToASCIIBytes(this string str) => str.ToBytes(Encoding.ASCII);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static string ToASCIIString(this byte[] bytes) => bytes.ConvertToString(Encoding.ASCII);

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static string ToBase64String(this byte[] bytes) => Convert.ToBase64String(bytes);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static byte[] ToBase64SourceBytes(this string str) => Convert.FromBase64String(str);

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static string ToHexString(this byte[] bytes) => BitConverter.ToString(bytes).Replace("-", "", StringComparison.Ordinal);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static string ToHexStringWithDash(this byte[] bytes) => BitConverter.ToString(bytes);

        #region numbers

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool IsShort(this string str) => short.TryParse(str, out _);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool TryConvertToShort(this string str, out short num) => short.TryParse(str, out num);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static short ToShort(this string str) => short.Parse(str);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static short ToShortOrZero(this string str) => short.TryParse(str, out var result) ? result : (short)0;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static short? ToShortOrNull(this string str) => short.TryParse(str, out var result) ? result : (short?)null;

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool IsUShort(this string str) => ushort.TryParse(str, out _);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool TryConvertToUShort(this string str, out ushort num) => ushort.TryParse(str, out num);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ushort ToUShort(this string str) => ushort.Parse(str);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ushort ToUShortOrZero(this string str) => ushort.TryParse(str, out var result) ? result : (ushort)0;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ushort? ToUShortOrNull(this string str) => ushort.TryParse(str, out var result) ? result : (ushort?)null;

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool IsDecimal(this string str) => decimal.TryParse(str, out _);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool TryConvertToDecimal(this string str, out decimal num) => decimal.TryParse(str, out num);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static decimal ToDecimal(this string str) => decimal.Parse(str);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static decimal ToDecimalOrZero(this string str) => decimal.TryParse(str, out var result) ? result : (decimal)0;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static decimal? ToDecimalOrNull(this string str) => decimal.TryParse(str, out var result) ? result : (decimal?)null;

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool IsInt(this string str) => int.TryParse(str, out _);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool TryConvertToInt(this string str, out int num) => int.TryParse(str, out num);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int ToInt(this string str) => int.Parse(str);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int ToIntOrZero(this string str) => int.TryParse(str, out var result) ? result : (int)0;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static int? ToIntOrNull(this string str) => int.TryParse(str, out var result) ? result : (int?)null;

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool IsUInt(this string str) => uint.TryParse(str, out _);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool TryConvertToUInt(this string str, out uint num) => uint.TryParse(str, out num);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint ToUInt(this string str) => uint.Parse(str);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint ToUIntOrZero(this string str) => uint.TryParse(str, out var result) ? result : (uint)0;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static uint? ToUIntOrNull(this string str) => uint.TryParse(str, out var result) ? result : (uint?)null;

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool IsLong(this string str) => long.TryParse(str, out _);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool TryConvertToLong(this string str, out long num) => long.TryParse(str, out num);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long ToLong(this string str) => long.Parse(str);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long ToLongOrZero(this string str) => long.TryParse(str, out var result) ? result : (long)0;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static long? ToLongOrNull(this string str) => long.TryParse(str, out var result) ? result : (long?)null;

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool IsULong(this string str) => ulong.TryParse(str, out _);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool TryConvertToULong(this string str, out ulong num) => ulong.TryParse(str, out num);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong ToULong(this string str) => ulong.Parse(str);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong ToULongOrZero(this string str) => ulong.TryParse(str, out var result) ? result : (ulong)0;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static ulong? ToULongOrNull(this string str) => ulong.TryParse(str, out var result) ? result : (ulong?)null;

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool IsFloat(this string str) => float.TryParse(str, out _);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool TryConvertToFloat(this string str, out float num) => float.TryParse(str, out num);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float ToFloat(this string str) => float.Parse(str);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float ToFloatOrZero(this string str) => float.TryParse(str, out var result) ? result : (float)0;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static float? ToFloatOrNull(this string str) => float.TryParse(str, out var result) ? result : (float?)null;

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool IsDouble(this string str) => double.TryParse(str, out _);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool TryConvertToDouble(this string str, out double num) => double.TryParse(str, out num);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static double ToDouble(this string str) => double.Parse(str);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static double ToDoubleOrZero(this string str) => double.TryParse(str, out var result) ? result : (double)0;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static double? ToDoubleOrNull(this string str) => double.TryParse(str, out var result) ? result : (double?)null;
            
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool IsInteger(this string str) => BigInteger.TryParse(str, out _);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool TryConvertToBigInteger(this string str, out BigInteger num) => BigInteger.TryParse(str, out num);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static BigInteger ToBigInteger(this string str) => BigInteger.Parse(str);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static BigInteger ToBigIntegerOrZero(this string str) => BigInteger.TryParse(str, out var result) ? result : (BigInteger)0;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static BigInteger? ToBigIntegerOrNull(this string str) => BigInteger.TryParse(str, out var result) ? result : (BigInteger?)null;

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool IsByte(this string str) => byte.TryParse(str, out _);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool TryConvertToByte(this string str, out byte num) => byte.TryParse(str, out num);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static byte ToByte(this string str) => byte.Parse(str);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static byte ToByteOrZero(this string str) => byte.TryParse(str, out var result) ? result : (byte)0;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static byte? ToByteOrNull(this string str) => byte.TryParse(str, out var result) ? result : (byte?)null;

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool IsSByte(this string str) => sbyte.TryParse(str, out _);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool TryConvertToSByte(this string str, out sbyte num) => sbyte.TryParse(str, out num);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static sbyte ToSByte(this string str) => sbyte.Parse(str);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static sbyte ToSByteOrZero(this string str) => sbyte.TryParse(str, out var result) ? result : (sbyte)0;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static sbyte? ToSByteOrNull(this string str) => sbyte.TryParse(str, out var result) ? result : (sbyte?)null;

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool IsChar(this string str) => char.TryParse(str, out _);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool TryConvertToChar(this string str, out char num) => char.TryParse(str, out num);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static char ToChar(this string str) => char.Parse(str);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static char ToCharOrZero(this string str) => char.TryParse(str, out var result) ? result : (char)0;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static char? ToCharOrNull(this string str) => char.TryParse(str, out var result) ? result : (char?)null;

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static string RemovePrefix(this string str, string prefix) => str.StartsWith(prefix, StringComparison.Ordinal) ? str.Substring(prefix.Length) : str;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static string RemovePostfix(this string str, string postfix) => str.EndsWith(postfix, StringComparison.Ordinal) ? str.SubstringFromLast(postfix.Length) : str;

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static Uri ToUri(this string str) => new Uri(str);

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static string GetFirstPart(this string source, char separator) => source.Split(separator).First();
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static string GetFirstPart(this string source, string separator) => source.Split(separator).First();

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static string GetLastPart(this string source, char separator) => source.Split(separator).Last();
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static string GetLastPart(this string source, string separator) => source.Split(separator).Last();

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static string[] Split(this string source, string separator, StringSplitOptions options = StringSplitOptions.None) => source.Split(separator.AsArray(), options);

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static string RemoveFirstChar(this string str) => str.Length > 0 ? str.Substring(1) : str;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static string RemoveLastChar(this string str) => str.Length > 0 ? str.SubstringFromLast(1) : str;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static string RemoveSurroundChars(this string str, int count = 1) => str.Length >= 2 * count ? str.Substring(count, str.Length - (count + 1)) : string.Empty;

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static string SubstringFromLast(this string str, int length) => str.Substring(0, str.Length - length);

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool IsNullOrWhiteSpace(this string str) => string.IsNullOrWhiteSpace(str);

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool NotNullNorEmpty(this string str) => !string.IsNullOrEmpty(str);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool NotNullNorWhiteSpace(this string str) => !string.IsNullOrWhiteSpace(str);

        public static string ToStringPlus<T>(this T obj)
        {
            if (obj == null) return "null";
            var originalToString = obj.ToString();
            
            return (originalToString != obj.GetType().ToString() || obj is Type ? originalToString : obj.ToJsonString()) ?? "null";
        }

        public static string? GetString(this SecureString value)
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