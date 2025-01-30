using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using GammaLibrary.Extensions;
#nullable enable

namespace GammaLibrary
{
    // TODO json太烂了 别用了
    public abstract class Configuration<T> where T : Configuration<T>, new()
    {
        private static T? _instance;
        static object locker = new();

        public static T Instance
        {
            get
            {
                lock (locker)
                {
                    if (_instance == null) Update();
                    return _instance!;
                }
            }
            protected set => _instance = value;
        }

        public static void Update()
        {
            lock (locker)
            {
                var savePath = SavePath;
                if (File.Exists(savePath))
                {
                    Instance = File.ReadAllText(savePath).JsonDeserialize<T>()!;
                }
                else
                {
                    Instance = new T();
                    Save();
                }

                Instance.OnUpdated();

            }
        }

        public static void Save()
        {
            lock (locker)
            {
                Instance.ToJsonString().SaveToFile(SavePath);
                Instance.OnSaved();
            }
        }

        protected virtual void OnUpdated() { }
        protected virtual void OnSaved() { }

        public static string SavePath => typeof(T).GetCustomAttribute<ConfigurationPathAttribute>()!.SaveName;
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class ConfigurationPathAttribute : Attribute
    {
        public string SaveName { get; }

        public ConfigurationPathAttribute(string saveName)
        {
            SaveName = saveName;
        }
    }

    [Obsolete("This attribute is obsolete, please use ConfigurationPathAttribute instead. And you should add '.json' to the end.")]
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ConfigurationAttribute : Attribute
    {
    }
}