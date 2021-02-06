using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GammaLibrary.Enhancements
{
    public readonly struct FilePath : IEquatable<FilePath>
    {
        public FilePath(string path, bool useFullPath = false)
        {
            SourcePath = useFullPath ? Path.GetFullPath(path) : path;
        }

        public string SourcePath { get; }

        /// <inheritdoc cref="Path.GetFullPath(string)"/>
        public string FullPath => Path.GetFullPath(SourcePath);

        /// <inheritdoc cref="Path.GetFileName(string)"/>
        public string FileName => Path.GetFileName(SourcePath);

        /// <inheritdoc cref="Path.GetFileNameWithoutExtension(string)"/>
        public string FileNameWithoutExtension => Path.GetFileNameWithoutExtension(SourcePath);

        /// <inheritdoc cref="Path.GetDirectoryName(string)"/>
        public string? DirectoryName => Path.GetDirectoryName(SourcePath);

        public static FilePath FromPathString(string s) => new(s);
        public static FilePath FromPathStringCreateFullPath(string s) => new(s, true);

        public static implicit operator string(FilePath path) => path.SourcePath;

        public static implicit operator FilePath(string path) => new(path);

        public override string ToString() => SourcePath;

        public bool Equals(FilePath other)
        {
            return SourcePath == other.SourcePath;
        }

        public override bool Equals(object? obj)
        {
            return obj is FilePath other && Equals(other);
        }

        public override int GetHashCode()
        {
            return SourcePath.GetHashCode(StringComparison.Ordinal);
        }

        public static bool operator ==(FilePath left, FilePath right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(FilePath left, FilePath right)
        {
            return !left.Equals(right);
        }
    }
}
