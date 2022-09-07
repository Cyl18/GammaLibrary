using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GammaLibrary.Common
{
    /// <summary>
    /// 
    /// </summary>
    /// <example>
    /// using (var measurer = new TimeMeasure)
    ///
    /// </example>
    public struct TimeMeasurer : IDisposable
    {
        readonly Stopwatch _stopwatch;
        readonly Action<string> _printer;
        readonly Func<TimeSpan, string> _formatter;
        static Action<string> ConsolePrinter { get; } = Console.WriteLine;
        static Func<TimeSpan, string> DefaultFormatter { get; } = e => "Completed operation in {:F2}s.";

        public TimeMeasurer(Func<TimeSpan, string>? formatter, Action<string>? printer = null)
        {
            formatter ??= DefaultFormatter;
            printer ??= ConsolePrinter;
            _printer = printer;
            _formatter = formatter;
            _stopwatch = Stopwatch.StartNew();
        }
        

        public TimeMeasurer(string formatStringInSeconds) : this(s => string.Format(CultureInfo.InvariantCulture, formatStringInSeconds, s.TotalSeconds))
        {
        }

        public void Report(string formatStringInSeconds)
        {
            _printer(string.Format(CultureInfo.InvariantCulture, formatStringInSeconds, _stopwatch.Elapsed.TotalSeconds));
        }
        
        public void ReportAndReset(string formatStringInSeconds)
        {
            _printer(string.Format(CultureInfo.InvariantCulture, formatStringInSeconds, _stopwatch.Elapsed.TotalSeconds));
            _stopwatch.Restart();
        }

        public void Dispose()
        {
            _printer(_formatter(_stopwatch.Elapsed));
        }
    }
}
