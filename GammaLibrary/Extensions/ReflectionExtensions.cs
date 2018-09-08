using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GammaLibrary.Extensions
{
    public static class ReflectionExtensions
    {
        public static bool IsAttributeDefined<T>(this MemberInfo info) =>
            info.CustomAttributes.Any(attribute => attribute.AttributeType == typeof(T));
    }
}
