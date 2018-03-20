using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Catalog.Models;
using Catalog.Services.Navigation;
using Xamarin.Forms;

namespace Catalog.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public ItemDetailViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            Title = "New Item";
        }

        private Item _item;
        public Item Item
        {
            get => _item;
            set => Set(ref _item, value);
        }

        public ICommand SaveItem => new Command(async () => await SaveItemCommand(), () => !IsBusy);

        private async Task SaveItemCommand()
        {
            MessagingCenter.Send(this, "AddItem", Item);
            await _navigationService.NavigateBackAsync();
        }

        public override Task InitializeAsync<TParam>(TParam parameter)
        {
            IsBusy = true;

            try
            {
                var item = parameter as Item;

                if (item != null)
                {
                    this.Item = item;
                    this.Title = item.Text;
                }

                return Task.FromResult(true);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}