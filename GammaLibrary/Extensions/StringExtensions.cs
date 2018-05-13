using System;
using System.Collections.Generic;
using System.Text;

namespace GammaLibrary.Extensions
{
    public static class StringExtensions
    {
        public static byte[] ToUTF8Bytes(this string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }

        public static string ToBase64String(this byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }
    }
}