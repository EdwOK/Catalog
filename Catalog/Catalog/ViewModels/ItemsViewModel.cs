using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Catalog.DataAccessLayer;
using Catalog.Infrastructure.Extensions;
using Catalog.Models;
using Catalog.Services.Navigation;
using Catalog.Views;
using Xamarin.Forms;

namespace Catalog.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly INavigationService _navigationService;

        public ItemsViewModel(INavigationService navigationService, UnitOfWork unitOfWork)
        {
            _navigationService = navigationService;
            _unitOfWork = unitOfWork;

            Title = "Browse";
            Items = new ObservableCollection<Item>();
            ItemSelectedCommand = new Command(ItemSelectedCommandExecute);
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

        void LoadItemsCommandExecute()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                Items = _unitOfWork.TestRepository.GetAll().ToObservable();
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
            LoadItemsCommandExecute();
        }
    }
}