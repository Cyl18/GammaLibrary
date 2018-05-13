using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using GammaLibrary.Extensions;

namespace GammaLibrary
{
    public abstract class Configuration<T> where T : Configuration<T>, new()
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null) Update();
                return _instance;
            }
            protected set => _instance = value;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Update()
        {
            try
            {
                var savePath = SavePath;
                if (FileSystem.Exists(savePath))
                {
                    Instance = FileSystem.ReadFile(savePath).JsonDeserialize<T>();
                }
                else
                {
                    Instance = new T();
                    Save();
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e, nameof(Configuration<T>));
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Save() => Instance.ToJsonString().SaveToFile(SavePath);

        public static string SavePath => typeof(T).GetCustomAttribute<ConfigurationAttribute>().SavePath;
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ConfigurationAttribute : Attribute
    {
        public string SavePath { get; }

        public ConfigurationAttribute(string savePath)
        {
            SavePath = savePath;
        }
    }
}