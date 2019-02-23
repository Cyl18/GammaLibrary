using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GammaLibrary.Extensions;

namespace GammaLibrary.Enhancements
{
    public static class Reflections
    {
        public static IEnumerable<Type> GetAssignableTypesInCurrentAssembly<T>() => 
            Assembly.GetCallingAssembly().GetAssignableTypes<T>();

        public static IEnumerable<Type> GetAssignableTypesInLoadedAssemblies<T>() =>
            AppDomain.CurrentDomain.GetAssemblies().SelectMany(ass => ass.GetAssignableTypes<T>(), (ass, collection) => collection);


    }
}
