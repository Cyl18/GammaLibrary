using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using Microsoft.CodeAnalysis;

namespace GammaLibrary.SourceGenerator
{
    [Generator]
    public class StringNumberInteractionGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {

        }

        public void Execute(GeneratorExecutionContext context)
        {
#if DEBUG && false
            if (!Debugger.IsAttached)
            {
                //Debugger.Launch();
            }
#endif 

            var sb = new StringBuilder();

            using var stream = new StreamReader(Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("GammaLibrary.SourceGenerator.NumberExtensionMethodsPrototype.txt")!);
            var prototype = stream.ReadToEnd();

            sb.AppendLine(@"
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace GammaLibrary.Extensions
{
    public static partial class StringExtensions
    {
");
            AddMethods("sbyte", "SByte", "NumberStyles.Integer");
            AddMethods("byte", "Byte", "NumberStyles.Integer");
            AddMethods("short", "Short", "NumberStyles.Integer");
            AddMethods("ushort", "UShort", "NumberStyles.Integer");
            AddMethods("int", "Int", "NumberStyles.Integer");
            AddMethods("uint", "UInt", "NumberStyles.Integer");
            AddMethods("long", "Long", "NumberStyles.Integer");
            AddMethods("ulong", "ULong", "NumberStyles.Integer");
            AddMethods("float", "Float", "NumberStyles.Float | NumberStyles.AllowThousands");
            AddMethods("double", "Double", "NumberStyles.Float | NumberStyles.AllowThousands");
            AddMethods("decimal", "Decimal", "NumberStyles.Number");
            AddMethods("BigInteger", "BigInteger", "NumberStyles.Integer");
            void AddMethods(string typeName, string typeString, string numberStyles)
            {
                var methodString = prototype
                    .Replace("$typename", typeName)
                    .Replace("$typestring", typeString)
                    .Replace("$styles", numberStyles);
                sb.AppendLine(methodString);
            }
            
            sb.AppendLine(@"
    }
}");
            context.AddSource(nameof(StringNumberInteractionGenerator), sb.ToString());
        }

        string GetNumberStyleDefaultValueString(MethodInfo methodInfo)
        {
            var parameterInfo = methodInfo.GetParameters()[1];
            var defaultStyles = (NumberStyles)parameterInfo.DefaultValue;

            var allNumberStyles = (NumberStyles[]) Enum.GetValues(typeof(NumberStyles));
            var numberStylesList = allNumberStyles
                .Where(style => (defaultStyles & style) == style)
                .Select(style => $"NumberStyles.{Enum.GetName(typeof(NumberStyles), style)}")
                .ToArray();
            
            return string.Join(" | ", numberStylesList);
        }
    }
}
