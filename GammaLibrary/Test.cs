using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using GammaLibrary.Extensions;

namespace GammaLibrary
{

    [Configuration("myconfig")]
    internal class MyConfig : Configuration<MyConfig>
    {
        public int IntConfig1 { get; set; } = 9;
        public string Str1 { get; set; } = "111";
        public Dictionary<string, int> Dic { get; set; } = new Dictionary<string, int>();

    }
}
