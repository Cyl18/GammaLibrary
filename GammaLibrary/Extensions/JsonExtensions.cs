using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GammaLibrary.Extensions
{
    public static class JsonExtensions
    {
        private static readonly SerializeSettings SerializeSettings = new ();
        public static bool UseLongRunning { get; set; }
        
        public static string ToJsonString<T>(this T source, JsonSerializerSettings? settings = null)
        {
            var realSettings = settings ?? SerializeSettings;
            return JsonConvert.SerializeObject(source, realSettings);
        }

        public static Task<string> ToJsonStringAsync<T>(this T source, JsonSerializerSettings? settings = null)
        {
            var realSettings = settings ?? SerializeSettings;
            return Task.Factory.StartNew(() => source.ToJsonString(realSettings),
                (UseLongRunning ? TaskCreationOptions.LongRunning : TaskCreationOptions.None) | TaskCreationOptions.HideScheduler);
        }
        
        public static T JsonDeserialize<T>(this string source, JsonSerializerSettings? settings = null)
        {
            var realSettings = settings ?? SerializeSettings;
            return JsonConvert.DeserializeObject<T>(source, realSettings) ?? throw new Exception("Json deserialize returned null.");
        }

        public static Task<T> JsonDeserializeAsync<T>(this string source, JsonSerializerSettings? settings = null)
        {
            var realSettings = settings ?? SerializeSettings;
            return Task.Factory.StartNew(() => source.JsonDeserialize<T>(realSettings),
                 (UseLongRunning ? TaskCreationOptions.LongRunning : TaskCreationOptions.None) | TaskCreationOptions.HideScheduler);
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