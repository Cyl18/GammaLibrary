using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GammaLibrary.Enhancements
{
    public static class Tasks
    {
        public static Task DelayAndExecuteAsync(int ms, Action action) => Task.Delay(ms).ContinueWith(t => action);
        public static Task DelayAndExecuteAsync(TimeSpan timeSpan, Action action) => Task.Delay(timeSpan).ContinueWith(t => action);

        public static Task<T> DelayAndExecuteAsync<T>(int ms, Func<T> action) => Task.Delay(ms).ContinueWith(t => action());
        public static Task<T> DelayAndExecuteAsync<T>(TimeSpan timeSpan, Func<T> action) => Task.Delay(timeSpan).ContinueWith(t => action());

        public static async Task DelayAndExecute<T>(int ms, Action action)
        {
            await Task.Delay(ms);
            action();
        }

        public static async Task DelayAndExecute<T>(TimeSpan timeSpan, Action action)
        {
            await Task.Delay(timeSpan);
            action();
        }

        public static async Task<T> DelayAndExecute<T>(int ms, Func<T> action)
        {
            await Task.Delay(ms);
            return action();
        }

        public static async Task<T> DelayAndExecute<T>(TimeSpan timeSpan, Func<T> action)
        {
            await Task.Delay(timeSpan);
            return action();
        }
    }
}
