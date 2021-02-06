using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Versioning;
using System.Text;
using GammaLibrary.Enhancements;

namespace GammaLibrary.Extensions
{
    public static class FileExtensions
    {
        public static void SaveToFile(this string content, FilePath path, Encoding? encoding = null)
        {
            var enc = encoding ?? Encoding.UTF8;
            File.WriteAllText(path, content, enc);
        }

        [SupportedOSPlatform("windows")]
        public static void ViewInExplorer(this FilePath path)
        {
            if (OperatingSystem.IsWindows())
            {
                Process.Start(new ProcessStartInfo { FileName = "explorer.exe", Arguments = $"/select,\"{path.FullPath}\"", UseShellExecute = false });
            }
        }

        public static void OpenWithShell(this FilePath path)
        {
            Process.Start(new ProcessStartInfo { FileName = path, UseShellExecute = true });
        }

        public static Process? Run(this FilePath path, string arguments = "")
        {
            return Process.Start(new ProcessStartInfo { FileName = path, Arguments = arguments, UseShellExecute = false });
        }
    }
}
