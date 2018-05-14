using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GammaLibrary
{
    public static class ResourceReader
    {
        /// <summary>
        /// 从指定汇编中读取资源.
        /// </summary>
        /// <param name="assembly">指定的汇编</param>
        /// <param name="name">资源名称</param>
        /// <returns>资源的字符串</returns>
        public static string Read(Assembly assembly, string name)
        {
            var stream = GetStream(assembly, name);
            return stream == null ? null : new StreamReader(stream).ReadToEnd();
        }

        public static Task<string> ReadAsync(Assembly assembly, string name)
        {
            var stream = GetStream(assembly, name);
            return stream == null ? null : new StreamReader(stream).ReadToEndAsync();
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

        public static Task<string> ReadAsync(string name)
        {
            var stream = GetStream(name);
            return stream == null ? null : new StreamReader(stream).ReadToEndAsync();
        }

        public static Stream GetStream(string name)
        {
            var currentAssembly = Assembly.GetCallingAssembly();
            return currentAssembly.GetManifestResourceStream(name);
        }
    }
}