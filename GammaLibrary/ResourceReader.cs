using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace GammaLibrary
{
    public static class ResourceReader
    {
        public static string Read(Assembly assembly, string name)
        {
            var stream = GetStream(assembly, name);
            return stream == null ? null : new StreamReader(stream).ReadToEnd();
        }

        public static Stream GetStream(Assembly assembly, string name)
        {
            return assembly.GetManifestResourceStream(name);
        }

        public static string Read(string name)
        {
            var stream = GetStream(name);
            return stream == null ? null : new StreamReader(stream).ReadToEnd();
        }

        public static Stream GetStream(string name)
        {
            var currentAssembly = Assembly.GetCallingAssembly();
            return currentAssembly.GetManifestResourceStream(name);
        }
    }
}