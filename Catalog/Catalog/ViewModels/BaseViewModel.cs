﻿using System;
using System.Windows.Input;
using Catalog.Services.Navigation;
using GalaSoft.MvvmLight;
using Xamarin.Forms;

namespace Catalog.ViewModels
{
    public abstract class BaseViewModel : ViewModelBase
    {
        protected INavigationService NavigationService;

        protected BaseViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => Set(ref _isBusy, value);
        }

        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        public ICommand AppearingCommand => new Command(AppearingCommandExecute);

        protected virtual void AppearingCommandExecute()
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

        protected virtual void Validate()
        {
        }

        private bool _disposed;

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