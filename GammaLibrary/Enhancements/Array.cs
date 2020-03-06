using System;
using System.Collections.Generic;
using System.Text;

namespace GammaLibrary.Enhancements
{
    public static class Arrays<T>
    {
        public static T[] Empty => new T[0];
        public static List<T> EmptyList => new List<T>();
    }
}
