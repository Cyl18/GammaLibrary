﻿using System;
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

        public static string ToUTF8String(this byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }

        public static string ToBase64String(this byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        public static byte[] ToBase64SourceBytes(this string str)
        {
            return Convert.FromBase64String(str);
        }

        public static string ToHexString(this byte[] bytes) => BitConverter.ToString(bytes).Replace("-", "");
    }
}