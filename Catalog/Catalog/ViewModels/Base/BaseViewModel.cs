using Catalog.Models;
using Catalog.Services;
using GalaSoft.MvvmLight;
using Xamarin.Forms;

namespace Catalog.ViewModels.Base
{
    public abstract class BaseViewModel : ViewModelBase
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>() ?? new MockDataStore();

        private bool _isBusy = false;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                Set(ref _isBusy, value);
                RaisePropertyChanged(() => IsBusy);
            }
        }

        string _title = string.Empty;
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }
    }
}
