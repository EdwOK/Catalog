using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Catalog.DataAccessLayer;
using Catalog.Models;
using Catalog.Services;
using Catalog.Services.Navigation;
using Catalog.Views;
using Xamarin.Forms;

namespace Catalog.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        //private readonly IDataStore<Item> _dataStore;
        private readonly UnitOfWork _unitOfWork;
        private readonly INavigationService _navigationService;

        public ItemsViewModel(IDataStore<Item> dataStore, INavigationService navigationService, UnitOfWork unitOfWork)
        {
            //_dataStore = dataStore;
            _navigationService = navigationService;
            _unitOfWork = unitOfWork;

            Title = "Browse";
            Items = new ObservableCollection<Item>();
            ItemSelectedCommand = new Command(ItemSelectedCommandExecute);

            MessagingCenter.Unsubscribe<ItemDetailViewModel, Item>(this, "AddItem");
            MessagingCenter.Subscribe<ItemDetailViewModel, Item>(this, "AddItem", (obj, item) =>
            {
                Items.Add(item);
                unitOfWork.TestRepository.Add(item);
                //await _dataStore.AddItemAsync(item);
            });
        }

        private ObservableCollection<Item> _items;
        public ObservableCollection<Item> Items
        {
            get => _items;
            set => Set(ref _items, value);
        }

        private Item _selectedItem;
        public Item SelectedItem
        {
            get => _selectedItem;
            set => Set(ref _selectedItem, value);
        }

        public Command ItemSelectedCommand { get; set; } 

        public ICommand LoadItemsCommand => new Command(LoadItemsCommandExecute);

        public ICommand AddItemCommand => new Command(async () => await ItemAddCommandExecute());

        public ICommand AppearingCommand => new Command(AppearingCommandExecute);

        private void LoadItemsCommandExecute()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                Items.Clear();

                //var items = await _dataStore.GetItemsAsync(true);
                var items = _unitOfWork.TestRepository.GetAll();
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

        private async void ItemSelectedCommandExecute()
        {
            if (SelectedItem == null)
            {
                return;
            }

            await _navigationService.NavigateToAsync<ItemDetailPage, ItemDetailViewModel, Item>(SelectedItem, false);
            SelectedItem = null;
        }

        private async Task ItemAddCommandExecute()
        {
            var item = new Item
            {
                Text = "Item name",
                Description = "This is an item description."
            };

            await _navigationService.NavigateToAsync<NewItemPage, ItemDetailViewModel, Item>(item, false);
        }

        private void AppearingCommandExecute()
        {
            if (Items.Count == 0)
            {
                LoadItemsCommandExecute();
            }
        }
    }
}