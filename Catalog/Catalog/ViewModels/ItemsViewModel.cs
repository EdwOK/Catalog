using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Catalog.Models;
using Catalog.Services;
using Catalog.Services.Navigation;
using Catalog.Views;
using Xamarin.Forms;

namespace Catalog.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private readonly IDataStore<Item> _dataStore;
        private readonly INavigationService _navigationService;

        public ItemsViewModel(IDataStore<Item> dataStore, INavigationService navigationService)
        {
            _dataStore = dataStore;
            _navigationService = navigationService;

            Title = "Browse";
            Items = new ObservableCollection<Item>();

            MessagingCenter.Subscribe<ItemDetailViewModel, Item>(this, "AddItem", async (obj, item) =>
            {
                Items.Add(item);
                await _dataStore.AddItemAsync(item);
            });
        }

        private ObservableCollection<Item> _items;
        public ObservableCollection<Item> Items
        {
            get => _items;
            set => Set(ref _items, value);
        }

        public ICommand ItemSelectedCommand => new Command<Item>(async (item) => await ExecuteItemSelectedCommand(item), item => !IsBusy);

        public ICommand LoadItemsCommand => new Command(async () => await ExecuteLoadItemsCommand(), () => !IsBusy);

        public ICommand AddItemCommand => new Command(async () => await ItemAddCommand(), () => !IsBusy);

        public ICommand AppearingCommand => new Command(async () => await AppearingCommandExecute(), () => !IsBusy);

        private async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();

                var items = await _dataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task ExecuteItemSelectedCommand(Item item)
        {
            if (item == null)
            {
                return;
            }

            await _navigationService.NavigateToAsync<ItemDetailPage, ItemDetailViewModel, Item>(parameter: item);
        }

        private async Task ItemAddCommand()
        {
            var item = new Item
            {
                Text = "Item name",
                Description = "This is an item description."
            };

            await _navigationService.NavigateToAsync<NewItemPage, ItemDetailViewModel, Item>(parameter: item);
        }

        private async Task AppearingCommandExecute()
        {
            if (Items.Count == 0)
            {
                await ExecuteLoadItemsCommand();
            }
        }
    }
}