using System.Threading.Tasks;
using Catalog.Core;
using Catalog.Models;
using Catalog.Services;
using Catalog.Services.Navigation;
using GalaSoft.MvvmLight;

namespace Catalog.ViewModels
{
    public abstract class BaseViewModel : ViewModelBase
    {
        protected readonly IDataStore<Item> DataStore;

        protected readonly INavigationService NavigationService;

        protected BaseViewModel()
        {
            DataStore = ViewModelLocator.Resolve<IDataStore<Item>>();
            NavigationService = ViewModelLocator.Resolve<INavigationService>();
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

        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }
    }
}
