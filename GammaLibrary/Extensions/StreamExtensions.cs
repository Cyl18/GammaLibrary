using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GammaLibrary.Extensions
{
    public static class StreamExtensions
    {
        public static string ReadToEnd(this Stream stream)
        {
            using var reader = stream.CreateStreamReader();
            return reader.ReadToEnd();
        }

        public static Task<string> ReadToEndAsync(this Stream stream)
        {
            using var reader = stream.CreateStreamReader();
            return reader.ReadToEndAsync();
        }

        public static StreamReader CreateStreamReader(this Stream stream, Encoding? encoding = null, bool detectEncodingFromByteOrderMarks = true, int bufferSize = -1, bool leaveOpen = false)
        {
            return new(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen);
        }

        public static StreamWriter CreateStreamWriter(this Stream stream, Encoding? encoding = null, int bufferSize = -1, bool leaveOpen = false)
        {
            return new(stream, encoding, bufferSize, leaveOpen);
        }

        public static void CopyToFile(this Stream stream, string path)
        {
            using var fileStream = File.OpenWrite(path);
            stream.CopyTo(fileStream);
        }

        /// <summary>
        /// Copies the <paramref name="source"/> stream to the <paramref name="target"/> stream
        /// </summary>
        /// <param name="target"></param>
        /// <param name="source"></param>
        public static T CopyFrom<T>(this T target, Stream source) where T : Stream
        {
            source.CopyTo(target);
            return target;
        }

        public static async Task<T> CopyFromAsync<T>(this T target, Stream source) where T : Stream
        {
            await source.CopyToAsync(target).ConfigureAwait(false);
            return target;
        }

        public static MemoryStream ToMemoryStream(this Stream stream)
        {
            return new MemoryStream().CopyFrom(stream);
        }
        // todo ? 不知道能不能用
        public static MemoryStream ToMemoryStreamAndDispose(this Stream stream)
        {
            var memoryStream = new MemoryStream().CopyFrom(stream);
            stream.Dispose();
            return memoryStream;
        }
    }
}
