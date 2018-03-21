using System;

namespace Catalog.Infrastructure.Utils
{
    public class Disposabled : IDisposable
    {
        protected bool IsDisposed { get; private set; }

        ~Disposabled()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void ThrowIfDisposed()
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    DisposeManagedResources();
                }

                DisposeNativeResources();
            }

            IsDisposed = true;
        }

        protected virtual void DisposeManagedResources()
        {
        }

        protected virtual void DisposeNativeResources()
        {
        }
    }
}
