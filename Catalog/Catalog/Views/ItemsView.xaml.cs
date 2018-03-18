using System;
using Catalog.Infrastructure;
using Catalog.Infrastructure.IoC;
using Catalog.Infrastructure.Locators;
using Xamarin.Forms;
using Catalog.Models;
using Catalog.ViewModels;

namespace Catalog.Views
{
	public partial class ItemsPage : ContentPage
	{
	    private ItemsViewModel _viewModel;

        public ItemsPage()
        {
            InitializeComponent();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is Item item))
            {
                return;
            }

            var viewModel = ViewModelLocator.Resolve<ItemDetailViewModel>();
            viewModel.Item = item;

            await Navigation.PushAsync(new ItemDetailPage(viewModel));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

	    async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }
    }
}