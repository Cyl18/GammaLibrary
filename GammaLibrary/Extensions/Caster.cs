using System;
using System.Collections.Generic;
using System.Text;

namespace GammaLibrary.Extensions
{
    public static class Caster
    {
        public static object ToObject<T>(this T obj) => obj;
    }
}
