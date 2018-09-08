using System;
using System.Collections.Generic;
using System.Text;

namespace GammaLibrary.Helpers
{
    public static class LazyHelper
    {
        public static Lazy<T> FromObject<T>(T obj) => new Lazy<T>(() => obj);
    }
}
