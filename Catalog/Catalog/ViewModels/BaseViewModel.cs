using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using Xamarin.Forms;

namespace Catalog.ViewModels
{
    public abstract class BaseViewModel : ViewModelBase
    {
        private bool _disposed;
        private bool _isBusy;

        private string _title = string.Empty;

        public bool IsBusy
        {
            get => _isBusy;
            set => Set(ref _isBusy, value);
        }

        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        public ICommand AppearingCommand => new Command(AppearingCommandExecute);

        public virtual void AppearingCommandExecute()
        {
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

        protected virtual bool IsValid()
        {
            return true;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                Cleanup();
            }

            _disposed = true;
        }
    }
}