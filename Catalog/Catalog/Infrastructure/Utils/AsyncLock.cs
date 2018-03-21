using System;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Utils
{
    public class AsyncLock
    {
        private readonly Task<IDisposable> _lockReleaseTask;
        private readonly SemaphoreSlim _lockSemaphore = new SemaphoreSlim(1, 1);

        public AsyncLock()
        {
            this._lockReleaseTask = Task.FromResult((IDisposable) new LockReleaser(this));
        }

        public Task<IDisposable> LockAsync()
        {
            return this.LockAsync(CancellationToken.None);
        }

        public Task<IDisposable> LockAsync(CancellationToken cancellationToken)
        {
            Task waitTask = _lockSemaphore.WaitAsync(cancellationToken);

            return waitTask.IsCompleted ?
                this._lockReleaseTask :
                waitTask.ContinueWith(
                    (t, releaser) => (IDisposable) releaser,
                    this._lockReleaseTask.Result,
                    cancellationToken,
                    TaskContinuationOptions.ExecuteSynchronously,
                    TaskScheduler.Default);
        }

        private class LockReleaser : IDisposable
        {
            private readonly AsyncLock _lockToRelease;

            internal LockReleaser(AsyncLock lockToRelease)
            {
                this._lockToRelease = lockToRelease;
            }

            public void Dispose()
            {
                this._lockToRelease._lockSemaphore.Release();
            }
        }
    }
}
