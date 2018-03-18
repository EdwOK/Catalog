using System;
using System.Threading.Tasks;
using Catalog.Models;
using Catalog.Services;
using Catalog.Services.Navigation;
using CommonServiceLocator;
using GalaSoft.MvvmLight;

namespace Catalog.ViewModels
{
    public abstract class BaseViewModel : ViewModelBase
    {
        protected readonly IDataStore<Item> DataStore;

        protected readonly INavigationService NavigationService;

        protected BaseViewModel()
        {
            DataStore = ServiceLocator.Current.GetInstance<IDataStore<Item>>();
            NavigationService = ServiceLocator.Current.GetInstance<INavigationService>();
        }

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

        public virtual void OnAppearing()
        {
        }

        public virtual void OnDisappearing()
        {
        }

        public virtual Task InitializeAsync(params object[] parameters)
        {
            return Task.FromResult(false);
        }
    }
}
