using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using GammaLibrary.Extensions;
#nullable enable

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

            Instance.OnUpdated();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Save()
        {
            Instance.ToJsonString().SaveToFile(SavePath);
            Instance.OnSaved();
        }

        protected virtual void OnUpdated() { }
        protected virtual void OnSaved() { }

        public static string SavePath => $"{typeof(T).GetCustomAttribute<ConfigurationAttribute>().SaveName}.json";
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ConfigurationAttribute : Attribute
    {
        public string SaveName { get; }

        public ConfigurationAttribute(string saveName)
        {
            SaveName = saveName;
        }
    }

}