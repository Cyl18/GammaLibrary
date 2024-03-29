﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GammaLibrary
{
    //[Obsolete("Use resource class instead.")]
    public static class ResourceReader
    {
        /// <summary>
        /// 从指定程序集中读取资源.
        /// </summary>
        /// <param name="assembly">指定的程序集</param>
        /// <param name="name">资源名称</param>
        /// <returns>资源的字符串</returns>
        public static string? Read(Assembly assembly, string name)
        {
            using var stream = GetStream(assembly, name);
            return stream == null ? null : new StreamReader(stream).ReadToEnd();
        }
        
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string? Read(string name) => Read(Assembly.GetCallingAssembly(), name);

        public static async Task<string?> ReadAsync(Assembly assembly, string name)
        {
            await using var stream = GetStream(assembly, name);
            return stream == null ? null : await new StreamReader(stream).ReadToEndAsync().ConfigureAwait(false);
        }
        [MethodImpl(MethodImplOptions.NoInlining)]

        public static Task<string?> ReadAsync(string name) => ReadAsync(Assembly.GetCallingAssembly(), name);

        public static Stream? GetStream(Assembly assembly, string name) => assembly.GetManifestResourceStream(name);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Stream? GetStream(string name) => GetStream(Assembly.GetCallingAssembly(), name);
    }
}