using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using GammaLibrary.Extensions;
using Newtonsoft.Json.Linq;

namespace GammaLibrary
{
    public class Localization // TODO database and JsonDatabase
    {
        private const string Separator = ".";
        private static Dictionary<string, string[]> Arrays { get; } = new Dictionary<string, string[]>();
        private static Dictionary<string, string> Values { get; } = new Dictionary<string, string>();

        public static bool ContainsValue(string key)
        {
            return Values.ContainsKey(key);
        }

        public static bool ContainsArray(string key)
        {
            return Arrays.ContainsKey(key);
        }

        public static string GetString(string key)
        {
            return Values.ContainsKey(key) ? Values[key] : key;
        }

        public static string[] GetArray(string key)
        {
            return Arrays.ContainsKey(key) ? Arrays[key] : new[] { key };
        }

        public static void Load(string json)
        {
#if TRACE
            var sw = Stopwatch.StartNew();
#endif
            Load(JToken.Parse(json), string.Empty);
#if TRACE
            Trace.WriteLine($"Localization file loaded, took {sw.ElapsedMilliseconds}ms");
#endif
        }

        private static void Load(JToken token, string key)
        {
            switch (token)
            {
                case JObject obj:
                    LoadInternal(obj, key);
                    break;

                case JArray array:
                    LoadInternal(array, key);
                    break;

                case JProperty property:
                    LoadInternal(property, key);
                    break;

                case JValue value:
                    LoadInternal(value, key);
                    break;

                default:
                    throw new NotSupportedException();
            }
        }

        private static void LoadInternal(JValue value, string key)
        {
            var valueStr = value.Value as string ?? throw new NotSupportedException();
            Values.Add(key, valueStr);
            Trace.WriteLine($"Loaded value: [key: {key}, value: {value}]", nameof(Localization));
        }

        private static void LoadInternal(JProperty property, string key)
        {
            key += string.IsNullOrEmpty(key) ? string.Empty : Separator;
            key += property.Name;
            Load(property.Value, key);
        }

        private static void LoadInternal(JArray array, string key)
        {
            var strArray = array.Select(token => ((JValue)token).Value as string ?? throw new NotSupportedException()).ToArray();
            if (Arrays.ContainsKey(key))
            {
                Arrays[key] = Arrays[key].Concat(strArray).ToArray();
            }
            else
            {
                Arrays.Add(key, strArray);
            }
            Trace.WriteLine($"Loaded array: [key: {key}, value: {strArray.Connect()}]", nameof(Localization));
        }

        private static void LoadInternal(JObject obj, string key)
        {
            obj.ForEach<JToken>(o => Load(o, key));
        }
    }
    
    

    public static class LocalizationExtensions
    {
        public static string ToLocalizedString(this string localizable)
        {
            return Localization.GetString(localizable);
        }
    }
}
