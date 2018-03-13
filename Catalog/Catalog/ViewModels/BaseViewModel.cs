
using Xamarin.Forms;

using Catalog.Models;
using Catalog.Services;
using MvvmCross.Core.ViewModels;

namespace Catalog.ViewModels
{
    public abstract class BaseViewModel : MvxViewModel
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>() ?? new MockDataStore();

        protected BaseViewModel()
        {
        }

        private bool _isBusy = false;

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        string _title = string.Empty;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
    }
}
