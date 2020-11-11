using System;
using System.Collections;

namespace GammaLibrary.Extensions
{
    public static class Saber
    {
        public static string ToStringX<T>(this T obj)
        {
            if (obj == null) return "null";
            var originalToString = obj.ToString();
            
            return (originalToString != obj.GetType().ToString() || obj is Type ? originalToString : obj.ToJsonString()) ?? "null";
        }
    }
}