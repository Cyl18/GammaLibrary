using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GammaLibrary
{
    public static class Benchmark
    {
        public static void MeasureTime(Action action, Action<TimeSpan> callback)
        {
            var sw = Stopwatch.StartNew();
            action();
            callback(sw.Elapsed);
        }
    }
}
