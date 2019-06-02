using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GammaLibrary
{
    // TODO Obsolete this
    public static class FileSystem
    {
        public static string ReadFile(string path)
        {
            return File.ReadAllText(GetPath(path));
        }

        public static bool Exists(string path)
        {
            return File.Exists(GetPath(path));
        }

        public static string GetDirectoryPath(string path)
        {
            return Path.GetDirectoryName(GetPath(path));
        }

        public static void EnsureDirectoryExists(string path)
        {
            var dire = GetDirectoryPath(path);
            if (!Directory.Exists(dire)) Directory.CreateDirectory(dire);
        }

        public static void SaveToFileIfNotExists(this byte[] bytes, string path)
        {
            File.WriteAllBytes(GetPath(path), bytes);
        }

        public static void SaveToFile(this string str, string path)
        {
            File.WriteAllText(GetPath(path), str, Encoding.UTF8);
        }

        public static string GetPath(string path)
        {
            return path;
        }
    }
}