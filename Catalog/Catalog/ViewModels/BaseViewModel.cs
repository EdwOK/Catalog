using System;
using GalaSoft.MvvmLight;

namespace Catalog.ViewModels
{
    public abstract class BaseViewModel : ViewModelBase, IDisposable
    {
        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => Set(ref _isBusy, value);
        }

        string _title = string.Empty;
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        ~BaseViewModel()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _isDisposed;

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            {
                Cleanup();
            }

            _isDisposed = true;
        }
    }
}
