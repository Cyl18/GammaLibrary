using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GammaLibrary.Common
{
    public struct AsyncLock : IDisposable
    {
        static SemaphoreSlim semaphore = new SemaphoreSlim(1);

        public async Task<AsyncLock> WaitAsync()
        {
            await semaphore.WaitAsync().ConfigureAwait(false);
            return this;
        }


        public void Dispose()
        {
            semaphore.Release();
        }
    }
}
