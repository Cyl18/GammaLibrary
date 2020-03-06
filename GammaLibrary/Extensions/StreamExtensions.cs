using System;
using System.Collections.Generic;
using System.IO;
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

        public static StreamReader CreateStreamReader(this Stream stream)
        {
            return new StreamReader(stream);
        }

        public static StreamWriter CreateStreamWriter(this Stream stream)
        {
            return new StreamWriter(stream);
        }
    }
}
