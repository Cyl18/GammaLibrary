using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GammaLibrary.Extensions
{
    public static class StreamExtensions
    {
        public static string ReadToEnd(this Stream stream)
        {
            return stream.CreateStreamReader().ReadToEnd();
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
