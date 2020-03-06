using System;
using System.Threading;
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
            if (action is null) throw new ArgumentNullException(nameof(action));
            await Task.Delay(timeSpan);
            action();
        }

        public static async Task<T> DelayAndExecute<T>(int ms, Func<T> action)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            await Task.Delay(ms);
            return action();
        }

        public static async Task<T> DelayAndExecute<T>(TimeSpan timeSpan, Func<T> action)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            await Task.Delay(timeSpan);
            return action();
        }

        public static async Task<TResult> WaitAsync<TResult>(this Task<TResult> task, TimeSpan timeout)
        {
            using var timeoutCancellationTokenSource = new CancellationTokenSource();
            var delayTask = Task.Delay(timeout, timeoutCancellationTokenSource.Token);
            if (await Task.WhenAny(task, delayTask) == task)
            {
                timeoutCancellationTokenSource.Cancel();
                return await task;
            }
            throw new TimeoutException("The operation has timed out.");
        }
    }
}
