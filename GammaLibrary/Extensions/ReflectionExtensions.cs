using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GammaLibrary.Extensions
{
    public static class ReflectionExtensions
    {
        public static bool IsAttributeDefined<T>(this MemberInfo info) where T : Attribute =>
            info.CustomAttributes.Any(attribute => attribute.AttributeType == typeof(T));

        public static T GetFirstAttribute<T>(this MethodInfo info, bool inherit = true) where T : Attribute => 
            info.GetCustomAttributes<T>(inherit).FirstOrDefault();

        public static bool IsAssignableFrom<T>(this Type type) => type.IsAssignableFrom(typeof(T));
        public static bool IsAssignableTo<T>(this Type type) => typeof(T).IsAssignableFrom(type);

        public static IEnumerable<Type> GetAssignableTypes<T>(this Assembly assembly) => 
            assembly.ExportedTypes.Where(type => type.IsAssignableTo<T>());
    }
}
