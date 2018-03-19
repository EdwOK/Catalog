using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Catalog.Models;
using Catalog.Views;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace Catalog.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new RelayCommand(async () => await ExecuteLoadItemsCommand(), () => !IsBusy);
            OnItemSelected = new RelayCommand<Item>(async item => await ItemSelectedCommand(item));
            AddItemCommand = new RelayCommand(async () => await ItemAddCommand());

            MessagingCenter.Subscribe<ItemsViewModel, Item>(this, "AddItem", async (obj, item) =>
            {
                Items.Add(item);
                await DataStore.AddItemAsync(item);
                await NavigationService.NavigateBackAsync(modal: true);
            });
        }

        public ObservableCollection<Item> Items { get; set; }

        public ICommand LoadItemsCommand { get; set; }

        public ICommand OnItemSelected { get; set; }

        public ICommand AddItemCommand { get; set; }

        private async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();

                var items = await DataStore.GetItemsAsync(true);
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

        private async Task ItemSelectedCommand(Item item)
        {
            if (item == null)
            {
                return;
            }

            await NavigationService.NavigateToAsync<ItemDetailPage, ItemDetailViewModel>();
        }

        private async Task ItemAddCommand()
        {
            var item = new Item
            {
                Text = "Item name",
                Description = "This is an item description."
            };

            await NavigationService.NavigateToAsync<NewItemPage, ItemDetailViewModel, Item>(modal: true, parameter: item);
        }

        public override async Task OnAppearing()
        {
            if (Items.Count == 0)
            {
                await ExecuteLoadItemsCommand();
            }
        }
    }
}