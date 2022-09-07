using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace GammaLibrary.Extensions
{
    public static class JsonExtensions
    {
        static readonly JsonSerializerOptions DefaultSerializerOptions = new()
        {
            IncludeFields = true,
            WriteIndented = true
        };
        
        public static string ToJsonString<T>(this T source, JsonSerializerOptions? options = null)
            => JsonSerializer.Serialize(source, options ?? DefaultSerializerOptions);

        public static Task WriteJsonAsync<T>(this Stream source, T data, JsonSerializerOptions? settings = null, CancellationToken token = default)
            => JsonSerializer.SerializeAsync(source, data, settings ?? DefaultSerializerOptions, token);

        public static Task WriteJsonToStreamAsync<T>(this T data, Stream source, JsonSerializerOptions? settings = null, CancellationToken token = default) 
            => JsonSerializer.SerializeAsync(source, data, settings ?? DefaultSerializerOptions, token);

        public static T JsonDeserialize<T>(this string source, JsonSerializerOptions? settings = null) 
            => JsonSerializer.Deserialize<T>(source, settings ?? DefaultSerializerOptions)!;

        public static T JsonDeserialize<T>(this ReadOnlySpan<byte> source, JsonSerializerOptions? settings = null) 
            => JsonSerializer.Deserialize<T>(source, settings ?? DefaultSerializerOptions)!;

        public static ValueTask<T?> JsonDeserializeAsync<T>(this Stream stream, JsonSerializerOptions? settings = null, CancellationToken token = default) 
            => JsonSerializer.DeserializeAsync<T>(stream, settings ?? DefaultSerializerOptions, token);
    }
    
}