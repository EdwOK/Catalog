﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Catalog.Models;
using Catalog.Views;
using MvvmCross.Core.ViewModels;

namespace Catalog.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public MvxObservableCollection<Item> Items { get; set; }
        public IMvxAsyncCommand LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new MvxObservableCollection<Item>();
            LoadItemsCommand = new MvxAsyncCommand(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                Items.Add(item);
                await DataStore.AddItemAsync(item);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
            {
                return;
            }

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
    }
}