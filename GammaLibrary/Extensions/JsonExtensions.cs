using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GammaLibrary.Extensions
{
    public static class JsonExtensions
    {
        private static readonly SerializeSettings SerializeSettings = new SerializeSettings();

        public static string ToJsonString<T>(this T source)
        {
            return JsonConvert.SerializeObject(source, SerializeSettings);
        }

        public static T JsonDeserialize<T>(this string source)
        {
            return JsonConvert.DeserializeObject<T>(source, SerializeSettings);
        }

        public static string ToJsonString<T>(this T source, JsonSerializerSettings settings)
        {
            return JsonConvert.SerializeObject(source, settings);
        }

        public static T JsonDeserialize<T>(this string source, JsonSerializerSettings settings)
        {
            return JsonConvert.DeserializeObject<T>(source, settings);
        }
    }

    public class SerializeSettings : JsonSerializerSettings
    {
        public SerializeSettings()
        {
            NullValueHandling = NullValueHandling.Include;
            Formatting = Formatting.Indented;
            MissingMemberHandling = MissingMemberHandling.Ignore;
        }
    }
}