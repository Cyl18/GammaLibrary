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

        public Task WaitAsync()
        {
            return semaphore.WaitAsync();
        }


        public void Dispose()
        {
            semaphore.Release();
        }
    }
}
