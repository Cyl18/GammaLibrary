using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using GammaLibrary.Extensions;
using GammaLibrary.Helpers;

namespace GammaLibrary
{
    public enum ResourceReadMode
    {
        String,
        Stream
    }

    public class Resource
    {
        private readonly Lazy<string> internalString;
        private readonly Lazy<Stream> internalStream;

        public static Resource FromFile(string path, ResourceReadMode mode = ResourceReadMode.String) =>
            mode == ResourceReadMode.String
                ? new Resource(File.ReadAllText(path))
                : new Resource(File.OpenRead(path));

        public static Resource FromManifestResource(string path, ResourceReadMode mode = ResourceReadMode.String) =>
            mode == ResourceReadMode.String
                ? new Resource(ResourceReader.Read(Assembly.GetCallingAssembly(), path) ?? throw new FileNotFoundException($"{path} is not found"))
                : new Resource(ResourceReader.GetStream(Assembly.GetCallingAssembly(), path) ?? throw new InvalidOperationException($"{path} stream is null"));

        public static Resource FromString(string str) => new Resource(str);

        public static Resource FromStream(Stream stream) => new Resource(stream);

        public string String => internalString.Value;
        public Stream Stream => internalStream.Value;

        public Resource(Stream stream)
        {
            internalStream = LazyHelper.FromObject(stream);
            internalString = new Lazy<string>(() => internalStream.Value.ReadToEnd());
        }

        public Resource(string str)
        {
            internalString = LazyHelper.FromObject(str);
            internalStream = new Lazy<Stream>(() => new MemoryStream(str.ToUTF8Bytes()));
        }

        public static implicit operator string(Resource resource) => resource.String;

        public static implicit operator Stream(Resource resource) => resource.Stream;
    }
}
