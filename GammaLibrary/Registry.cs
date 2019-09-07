using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using GammaLibrary.Extensions;

namespace GammaLibrary
{
    // TODO 写法有问题 要分两个类.
    public class Registry<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        private readonly Func<TValue, TKey> keySelector;
        private readonly Dictionary<TKey, TValue> registry = new Dictionary<TKey, TValue>();
        public event Action<TKey, TValue> OnRegister;

        public Registry(Func<TValue, TKey> keySelector)
        {
            this.keySelector = keySelector;
        }

        public void Register(TValue value)
        {
            Register(keySelector(value), value);
        }

        public void Register(TKey key, TValue value)
        {
            registry.Add(key, value);
            OnRegister?.Invoke(key, value);
        }

        public TValue Get(TKey key) => registry[key];

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return registry.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class Registry<TValue> : IEnumerable<TValue>
    {
        private readonly HashSet<TValue> registry = new HashSet<TValue>();
        public event Action<TValue> OnRegister;

        public void Register(TValue value)
        {
            registry.Add(value);
            OnRegister?.Invoke(value);
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            return registry.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    public class TypeRegistry<TType> : IEnumerable<Type>
    {
        private readonly HashSet<Type> registry = new HashSet<Type>();
        public event Action<Type> OnRegister;

        public void Register<T>() where T : TType
        {
            Register(typeof(T));
        }

        public void Register(Type type)
        {
            registry.Add(type);
            OnRegister?.Invoke(type);
        }

        public void RegisterAll()
        {
            Assembly.GetCallingAssembly().ExportedTypes
                .Where(type => type.IsSubclassOf(typeof(TType)) && type.IsClass)
                .ForEach(Register);
        }

        public IEnumerator<Type> GetEnumerator()
        {
            return registry.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
