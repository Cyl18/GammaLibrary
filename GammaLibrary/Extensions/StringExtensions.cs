using System;
using System.Collections.Generic;
using System.Globalization;
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
    public static partial class StringExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static string OrEmpty(this string? s) => s ?? string.Empty;
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static byte[] ToBytes(this string str, Encoding encoding) => encoding.GetBytes(str);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static string ConvertToString(this byte[] bytes, Encoding encoding) => encoding.GetString(bytes);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static byte[] ToUTF8Bytes(this string str) => str.ToBytes(new UTF8Encoding(false));
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool NotStartsWith(this string str, string value, StringComparison stringComparison = StringComparison.Ordinal) => !str.StartsWith(value, stringComparison);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool NotStartsWith(this string str, char value) => !str.StartsWith(value);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool NotEndsWith(this string str, string value, StringComparison stringComparison = StringComparison.Ordinal) => !str.EndsWith(value, stringComparison);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool NotEndsWith(this string str, char value) => !str.EndsWith(value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool EqualsIgnoreCase(this string str, string other) => str.Equals(other, StringComparison.OrdinalIgnoreCase);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool NotEqualsIgnoreCase(this string str, string other) => !str.Equals(other, StringComparison.OrdinalIgnoreCase);

        

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