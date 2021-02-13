using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GammaLibrary.Extensions
{
    public static class NullExtensions
    {
        public static T OrDefault<T>(this T? obj) where T : struct
        {
            return obj ?? default;
        }
    }
}
