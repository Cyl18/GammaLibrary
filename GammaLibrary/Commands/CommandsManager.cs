using System;
using System.Linq;
using System.Reflection;
using GammaLibrary.Extensions;

namespace GammaLibrary.Commands
{
    public class CommandsManager
    {
        private static Registry<CommandInfo> commands = new Registry<CommandInfo>();

        public static void RegisterAll()
        {
            Assembly.GetCallingAssembly().ExportedTypes
                .Where(type => type.IsAttributeDefined<CommandContainerAttribute>() && type.IsClass)
                .ForEach(Register);
        }

        private static void Register(Type obj)
        {
            obj.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                .Where(method => method.IsAttributeDefined<CommandAttribute>()).ForEach(RegisterMethod);
        }

        private static void RegisterMethod(MethodInfo info)
        {
            commands.Register(new CommandInfo(info));
        }
    }

    internal class CommandInfo
    {
        public CommandInfo(MethodInfo method)
        {
            
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class CommandAttribute : Attribute
    {
        public CommandAttribute()
        {
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class CommandContainerAttribute : Attribute
    {
        public CommandContainerAttribute()
        {
        }
    }
}
