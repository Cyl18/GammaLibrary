using System;
using System.Diagnostics;

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
