﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace GammaLibrary.Extensions
{
    public static class StreamExtensions
    {
        // TODO optional dispose
        public static string ReadToEnd(this Stream stream)
        {
            var reader = stream.CreateStreamReader();
            return reader.ReadToEnd();
        }

        public static Task<string> ReadToEndAsync(this Stream stream)
        {
            var reader = stream.CreateStreamReader();
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

        public static void CopyToFile(this Stream stream, string path)
        {
            using var fileStream = File.OpenWrite(path);
            stream.CopyTo(fileStream, 4096);
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

        public static MemoryStream ToMemoryStream(this Stream stream)
        {
            return new MemoryStream().CopyFrom(stream);
        }

        public static MemoryStream ToMemoryStreamAndDispose(this Stream stream)
        {
            var memoryStream = new MemoryStream().CopyFrom(stream);
            stream.Dispose();
            return memoryStream;
        }
    }
}
