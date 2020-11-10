using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GammaLibrary.Extensions
{
    public static class FileExtensions
    {
        public static void SaveToFile(this string content, string path, Encoding? encoding = null)
        {
            var enc = encoding ?? Encoding.UTF8;
            File.WriteAllText(path, content, enc);
        }

    }
}
