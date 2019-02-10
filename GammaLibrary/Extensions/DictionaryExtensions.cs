using System;
using System.Collections.Generic;
using System.Text;

namespace GammaLibrary.Extensions
{
    public static class DictionaryExtensions
    {
        public static bool TryGet<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, out TValue value) 
        {
            var flag = dictionary.ContainsKey(key);
            value = flag ? dictionary[key] : default;
            return flag;
        }
    }
}
